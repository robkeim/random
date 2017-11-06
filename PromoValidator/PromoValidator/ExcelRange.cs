using System;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace PromoValidator
{
    public class ExcelRange : IDisposable
    {
        private Excel.Application _app;
        private Excel.Workbook _workbook;
        private Excel._Worksheet _worksheet;
        
        public Excel.Range Range { get; }

        public ExcelRange(string fileName)
        {
            _app = new Excel.Application();
            _workbook = _app.Workbooks.Open(fileName);
            _worksheet = _workbook.Sheets[1];
            Range = _worksheet.UsedRange;
        }

        public void Dispose()
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(Range);
            Marshal.ReleaseComObject(_worksheet);
            _workbook.Save();
            _workbook.Close();
            Marshal.ReleaseComObject(_workbook);
            _app.Quit();
            Marshal.ReleaseComObject(_app);
        }
    }
}
