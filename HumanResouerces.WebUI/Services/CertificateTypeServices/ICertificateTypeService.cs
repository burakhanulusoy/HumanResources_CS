using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.CertificateTypeDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.CertificateTypeServices
{
    public interface ICertificateTypeService : IGenericService<ResultCertificateTypeDto, CreateCertificateTypeDto, UpdateCertificateTypeDto>
    {
        Task<BaseResult<List<CertificateTypeDto>>> GetAllWithCertificatesAsync();
        Task<BaseResult<CertificateTypeDto>> GetWithCertificatesAsync(int id);
    }
}