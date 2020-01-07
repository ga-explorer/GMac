using System;
using System.IO;
using Microsoft.CSharp.RuntimeBinder;
using OfficeOpenXml;

namespace GMac.GMacAST.Visitors.Excel
{
    public abstract class GMacAstToExcelConverter 
        : IAstObjectDynamicVisitor, IDisposable
    {
        private ExcelPackage _package;

        public ExcelPackage Package 
            => _package ?? (_package = InitExcelPackage());

        public ExcelWorkbook Workbook 
            => Package?.Workbook;

        public ExcelWorksheets Worksheets 
            => Workbook?.Worksheets;

        public bool UseExceptions => true;

        public bool IgnoreNullElements => false;


        public virtual void Fallback(IAstObject objItem, RuntimeBinderException excException)
        {
        }


        protected virtual ExcelPackage InitExcelPackage()
        {
            var package = new ExcelPackage();

            package.Compatibility.IsWorksheets1Based = false;

            return package;
        }

        public void SaveToFile(string filePath)
        {
            Package.SaveAs(new FileInfo(filePath));
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _package?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
