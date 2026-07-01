using HumanResources.DataAccess.Repositories;
using HumanResources.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.DataAccess.Repositories.CertificateRepositories
{
    public interface ICertificateRepository : IGenericRepository<Sertifika>
    {
        // 1. Personelin profiline girince aldığı tüm sertifikaları getir
        Task<List<Sertifika>> GetCertificateByUserIdAsync(int userId);

        // 2. Bildirim servisi için bitme süresi yaklaşanları getir
        Task<List<Sertifika>> GetDateUpcamingSoonAsync(int bildirimGunu);

        // YENİ 3. Admin bir sertifika türüne tıkladığında o belgeyi alan personelleri getir
        Task<List<Sertifika>> GetUsersByCertificateTypeIdAsync(int sertifikaTuruId);
    }
}