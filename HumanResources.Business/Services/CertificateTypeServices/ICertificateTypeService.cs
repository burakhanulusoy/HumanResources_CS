using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.CertificateTypeServices
{
    public interface ICertificateTypeService : IGenericService<ResultCertificateTypeDto, CreateCertificateTypeDto, UpdateCertificateTypeDto>
    {
        //  tüm sertifika türlerini ve o türden belge alanları listeleyeceği metot
        Task<BaseResult<List<CertificateTypeDto>>> GetAllCertificateTypeWithCertificateAsync();

        Task<BaseResult<CertificateTypeDto>> GetCertificateTypeWithCertificateAsync(int id);
    }
}