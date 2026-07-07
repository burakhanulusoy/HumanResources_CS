using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.CertificateDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.CertificateServices
{
    public interface ICertificateService : IGenericService<ResultCertificateDto, CreateCertificateDto, UpdateCertificateDto>
    {
        Task<BaseResult<List<CertificateDto>>> GetByUserIdAsync(int userId);
        Task<BaseResult<List<CertificateDto>>> GetUpcomingSoonAsync(int days);
        Task<BaseResult<List<CertificateDto>>> GetByCertificateTypeIdAsync(int typeId);
    }
}