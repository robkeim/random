using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

            var fileName = GetFileName();

            if (fileName != null)
            {
                using (var excelRange = new ExcelRange(fileName))
                {
                    var products = ReadProducts(excelRange.Range);
                    HydrateProducts(products);

                    WriteProducts(products, excelRange.Range);
                }
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

        private static Product[] ReadProducts(Excel.Range range)
        {
            var products = new List<Product>();
            
            for (int i = 2; i <= range.Rows.Count; i++)
            {
                string sku = range.Cells[i, 1]?.Value2?.ToString();

                if (sku == null)
                {
                    break;
                }

                string expectedPrice = range.Cells[i, 2]?.Value2?.ToString();
                string productPageUrl = range.Cells[i, 4]?.Value2?.ToString();

                var product = new Product
                {
                    RowNum = i,
                    Sku = sku,
                    ExpectedPrice = double.Parse(expectedPrice), // TODO rkeim add check for invalid value here
                    ProductPageUrl = productPageUrl
                };

                products.Add(product);
            }

            return products.ToArray();
        }

        private static void HydrateProducts(Product[] products)
        {
            ServicePointManager.UseNagleAlgorithm = true;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.CheckCertificateRevocationList = false;
            ServicePointManager.DefaultConnectionLimit = 128;

            Regex priceRegex = new Regex("<span id=\"product_price\" class=\"hidden\">(.*?)</span>", RegexOptions.Singleline | RegexOptions.Compiled);

            int productsProcessed = 0;

            Parallel.ForEach(products, product =>
            {
                try
                {
                    using (var webRequest = new WebClient())
                    {
                        webRequest.Proxy = null;

                        var url = $"http://www.lazada.co.th/catalog/?q={product.Sku}";
                        var html = webRequest.DownloadString(url);

                        var productPageUrlRegex = new Regex($"data-sku-simple=\"{product.Sku}\".*?url: '(.*?)'", RegexOptions.Singleline);

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

        private static void WriteProducts(Product[] products, Excel.Range range)
        {
            foreach (var product in products)
            {
                if (product.ProductPageUrl != null)
                {
                    range.Cells[product.RowNum, 3] = product.ActualPrice;
                    range.Hyperlinks.Add(range.Cells[product.RowNum, 4], product.ProductPageUrl);

                    if (product.ActualPrice < product.ExpectedPrice)
                    {
                        range.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbOrange;
                    }
                    else if (product.ActualPrice > product.ExpectedPrice)
                    {
                        range.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbRed;
                    }
                    else
                    {
                        // No need to color the cell if the prices are equal
                    }
                }
                else
                {
                    range.Cells[product.RowNum, 4] = "Product not found";
                    range.Cells[product.RowNum, 3] = "N/A";
                    range.Cells[product.RowNum, 3].Interior.Color = Excel.XlRgbColor.rgbYellow;
                }
            }
        }
    }
}
