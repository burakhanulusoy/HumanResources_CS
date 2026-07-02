namespace HumanResources.WebUI.Services.ReportServices
{
    public interface IReportService
    {
        Task<byte[]> GetPersonnelListExcelAsync();
        Task<byte[]> GetDepartmentBasedPersonnelExcelAsync();
        Task<byte[]> GetActivePassivePersonnelExcelAsync();
        Task<byte[]> GetPermissionReportExcelAsync();
        Task<byte[]> GetEducationReportExcelAsync();
        Task<byte[]> GetCertificateReportExcelAsync();
        Task<byte[]> GetItemReportExcelAsync();
        Task<byte[]> GetShiftReportExcelAsync();
    }
}