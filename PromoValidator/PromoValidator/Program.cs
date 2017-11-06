using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace PromoValidator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            var filename = GetFileName();

            if (filename != null)
            {
                var products = ReadProducts(filename);

                HydrateProducts(products);

                WriteProducts(products, filename);
            }

            Console.WriteLine($"\nDone in {stopwatch.Elapsed.Minutes} minutes and {stopwatch.Elapsed.Seconds} seconds");
            Console.ReadLine();
        }

        private static string GetFileName()
        {
            var files = Directory.GetFiles(".", "*.xls*");

            if (files.Length == 0)
            {
                Console.WriteLine("No available Excel files in this directory");
                return null;
            }

            if (files.Length > 1)
            {
                Console.WriteLine("Multiple Excel files in the current directly.\nPlease ensure only the Excel file you want to process is in the directory.");
                return null;
            }

            return Path.GetFullPath(files.Single());
        }

        private static Product[] ReadProducts(string filename)
        {
            var products = new List<Product>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filename);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            for (int i = 2; i <= rowCount; i++)
            {
                string sku = xlRange.Cells[i, 1]?.Value2?.ToString();

                if (sku == null)
                {
                    break;
                }

                string expectedPrice = xlRange.Cells[i, 2]?.Value2?.ToString();
                string productPageUrl = xlRange.Cells[i, 4]?.Value2?.ToString();

                var product = new Product
                {
                    RowNum = i,
                    Sku = sku,
                    ExpectedPrice = double.Parse(expectedPrice), // TODO rkeim add check for invalid value here
                    ProductPageUrl = productPageUrl
                };

                products.Add(product);
            }

            //cleanup
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return products.ToArray();
        }

        private static void HydrateProducts(Product[] products)
        {
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.DefaultConnectionLimit = 128;

            Regex priceRegex = new Regex("<span id=\"product_price\" class=\"hidden\">(.*?)</span>",
                RegexOptions.Singleline | RegexOptions.Compiled);

            int productsProcessed = 0;

            Parallel.ForEach(products, product =>
            {
                try
                {
                    using (var webRequest = new WebClient())
                    {
                        webRequest.Proxy = null;

                        // aCcept header: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
                        // Accept encoding: gzip, deflate, br

                        //webRequest.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.75 Safari/537.36");
                        //webRequest.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                        var url = $"http://www.lazada.co.th/catalog/?q={product.Sku}";
                        var html = webRequest.DownloadString(url);

                        var productPageUrlRegex = new Regex($"data-sku-simple=\"{product.Sku}\".*?url: '(.*?)'",
                            RegexOptions.Singleline);

                        var match = productPageUrlRegex.Match(html);

                        if (match.Success)
                        {
                            product.ProductPageUrl = $"http://www.lazada.co.th{match.Groups[1]}";
                            html = webRequest.DownloadString(product.ProductPageUrl);

                            match = priceRegex.Match(html);

                            if (!match.Success)
                            {
                                Console.WriteLine("No price found");
                            }
                            else
                            {
                                product.ActualPrice = double.Parse(match.Groups[1].ToString());
                            }
                        }
                        else
                        {
                            product.ProductPageUrl = null;
                        }

                        // Need to see if I can find the product sku here (can I use mobile here and desktop for the second request?)
                        Console.SetCursorPosition(0, Console.CursorTop);
                        // Change to interlocked increment
                        Console.Write($"Processing {++productsProcessed} of {products.Length}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e}");
                }
            });
        }

        private static void WriteProducts(Product[] products, string filename)
        {
            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filename);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            foreach (var product in products)
            {
                if (product.ProductPageUrl != null)
                {
                    xlRange.Cells[product.RowNum, 3] = product.ActualPrice;
                    xlRange.Hyperlinks.Add(xlRange.Cells[product.RowNum, 4], product.ProductPageUrl);

                    if (product.ActualPrice < product.ExpectedPrice)
                    {
                        xlRange.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbOrange;
                    }
                    else if (product.ActualPrice > product.ExpectedPrice)
                    {
                        xlRange.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbRed;
                    }
                    else
                    {
                        // No need to color the cell if the prices are equal
                    }
                }
                else
                {
                    xlRange.Cells[product.RowNum, 4] = "Product not found";
                    xlRange.Cells[product.RowNum, 3] = "N/A";
                    xlRange.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbYellow;
                }
            }

            //cleanup
            //GC.Collect();
            //GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Save();
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
        }
    }
}
