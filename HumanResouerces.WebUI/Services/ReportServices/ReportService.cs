using HumanResouerces.WebUI.Base; // Senin projedeki tam yazýlýmýyla ekledik
using HumanResouerces.WebUI.Exceptions;

namespace HumanResources.WebUI.Services.ReportServices
{
    public class ReportService(HttpClient _client) : IReportService
    {
        private async Task<byte[]> DownloadExcelAsync(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);

            // Ýstek baþarýlýysa doðrudan byte dizisini (Excel dosyasýný) döndür
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsByteArrayAsync();
            }

            // Baþarýsýzsa API'dan gelen hatalarý List<Error> formatýnda oku
            var errors = await response.Content.ReadFromJsonAsync<List<Error>>();

            // C# derleyicisinin kafasýnýn karýþmamasý için açýkça new List<Error>() kullanýyoruz
            throw new ApiValidationException(errors ?? new List<Error>());
        }

        public async Task<byte[]> GetPersonnelListExcelAsync()
        {
            return await DownloadExcelAsync("reports/personnel-list");
        }

        public async Task<byte[]> GetDepartmentBasedPersonnelExcelAsync()
        {
            return await DownloadExcelAsync("reports/department-personnel");
        }

        public async Task<byte[]> GetActivePassivePersonnelExcelAsync()
        {
            return await DownloadExcelAsync("reports/active-passive-personnel");
        }

        public async Task<byte[]> GetPermissionReportExcelAsync()
        {
            return await DownloadExcelAsync("reports/permissions");
        }

        public async Task<byte[]> GetEducationReportExcelAsync()
        {
            return await DownloadExcelAsync("reports/educations");
        }

        public async Task<byte[]> GetCertificateReportExcelAsync()
        {
            return await DownloadExcelAsync("reports/certificates");
        }

        public async Task<byte[]> GetItemReportExcelAsync()
        {
            return await DownloadExcelAsync("reports/items");
        }

        public async Task<byte[]> GetShiftReportExcelAsync()
        {
            return await DownloadExcelAsync("reports/shifts");
        }
    }
}