using OfficeOpenXml; // EKSÝK OLAN SATIR BURASI

namespace HumanResources.Business.Helpers
{
    public static class ExcelHelper
    {
        public static byte[] CreateExcel<T>(List<T> data, string sheetName)
        {
            // EPPlus ticari olmayan kullaným lisans onayý
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // Veriyi A1 hücresinden itibaren baþlýklarla birlikte (true) doldur
            worksheet.Cells["A1"].LoadFromCollection(data, true);

            // Sütun geniþliklerini içindeki yazýya göre otomatik ayarla
            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        }
    }
}