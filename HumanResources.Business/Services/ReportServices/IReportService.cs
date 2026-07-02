using HumanResources.Business.Base;

namespace HumanResources.Business.Services.ReportServices
{
    public interface IReportService
    {
        Task<BaseResult<byte[]>> GetPersonnelListExcelAsync();
        Task<BaseResult<byte[]>> GetDepartmentBasedPersonnelExcelAsync();
        Task<BaseResult<byte[]>> GetActivePassivePersonnelExcelAsync();
        Task<BaseResult<byte[]>> GetPermissionReportExcelAsync();
        Task<BaseResult<byte[]>> GetEducationReportExcelAsync();
        Task<BaseResult<byte[]>> GetCertificateReportExcelAsync();
        Task<BaseResult<byte[]>> GetItemReportExcelAsync(); // Zimmet
        Task<BaseResult<byte[]>> GetShiftReportExcelAsync(); // Vardiya
    }
}