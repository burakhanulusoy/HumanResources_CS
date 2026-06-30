using HumanResources.DataAccess.Repositories;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;

namespace HumanResources.DataAccess.Repositories.UserEducationRepositories
{
    public interface IUserEducationRepository : IGenericRepository<AppUserEgitim>
    {
        // Admin onay ekranı için belirli durumdaki (örneğin 'Bekliyor') başvuruları getirme
        Task<List<AppUserEgitim>> GetApplicationStatusAsync(ApplicationStatus durum);

        // Bir personelin aldığı/başvurduğu tüm eğitimleri getirme (Örn: Profil sayfası)
        Task<List<AppUserEgitim>> GetEducationByUserIdAsync(int userId);

        // Spesifik bir eğitime başvuran tüm personelleri getirme (Örn: Eğitim Detay Sayfası)
        Task<List<AppUserEgitim>> GetUsersByEducationIdAsync(int egitimId);

        Task<List<AppUserEgitim>> GetUserEducationWithAllInfoAsync();


    }
}