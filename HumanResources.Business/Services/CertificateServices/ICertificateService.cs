using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.CertificateServices
{
    public interface ICertificateService : IGenericService<ResultCertificateDto, CreateCertificateDto, UpdateCertificateDto>
    {
        // 1. Personelin profiline girince aldığı tüm sertifikaları getir
        Task<BaseResult<List<CertificateDto>>> GetCertificateByUserIdAsync(int userId);

        // 2. Bildirim servisi için bitme süresi yaklaşanları getir
        Task<BaseResult<List<CertificateDto>>> GetDateUpcamingSoonAsync(int bildirimGunu);

        // 3. Admin bir sertifika türüne tıkladığında o belgeyi alan personelleri getir
        Task<BaseResult<List<CertificateDto>>> GetUsersByCertificateTypeIdAsync(int sertifikaTuruId);

        // ICertificateService.cs — ekle
        Task<BaseResult<List<CertificateDto>>> GetAllWithInfoAsync();
        Task<BaseResult<CertificateDto>> GetByIdWithInfoAsync(int id);

    }
}   