using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.CertificateRepositories
{
    public class CertificateRepository : GenericRepository<Sertifika>, ICertificateRepository
    {
        public CertificateRepository(AppDbContext _context) : base(_context)
        {
        }

        public async Task<List<Sertifika>> GetCertificateByUserIdAsync(int userId)
        {
            return await _table
                 .Include(x => x.AppUser)       // Personelin bilgileri gelsin
                 .Include(x => x.SertifikaTuru) // UI'da göstermek için Sertifika adý gelsin
                 .Where(x => x.AppUserId == userId)
                 .AsNoTracking()
                 .ToListAsync();
        }

        public async Task<List<Sertifika>> GetDateUpcamingSoonAsync(int bildirimGunu)
        {
            var hedefTarih = DateTime.UtcNow.AddDays(bildirimGunu);

            return await _table
                .Include(x => x.AppUser)
                .Include(x => x.SertifikaTuru)
                .Where(x =>
                    (x.Durumu == Entity.Enums.CertificateStatus.Gecerli && x.GecerlilikTarihi <= hedefTarih) ||
                    x.Durumu == Entity.Enums.CertificateStatus.SuresiDolu ||
                    x.Durumu == Entity.Enums.CertificateStatus.IptalEdildi)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Sertifika>> GetUsersByCertificateTypeIdAsync(int sertifikaTuruId)
        {
            return await _table
                .Include(x => x.AppUser)       // Kimlerin aldýðýný görmek için User'ý baðlýyoruz
                .Include(x => x.SertifikaTuru) // Ýlgili sertifikanýn detaylarýný baðlýyoruz
                .Where(x => x.SertifikaTuruId == sertifikaTuruId)
                .AsNoTracking()
                .ToListAsync();
        }

        // CertificateRepository.cs — ekle
        public async Task<List<Sertifika>> GetAllWithInfoAsync()
        {
            return await _table
                .Include(x => x.AppUser)
                .Include(x => x.SertifikaTuru)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Sertifika> GetByIdWithInfoAsync(int id)
        {
            return await _table
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Departman)
                .Include(x => x.AppUser)
                    .ThenInclude(u => u.Birim)
                .Include(x => x.SertifikaTuru)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        // CertificateRepository.cs
        public async Task<List<Sertifika>> GetExpiredButStillValidAsync()
        {
            return await _table
                .Where(x => x.Durumu == CertificateStatus.Gecerli &&
                            x.GecerlilikTarihi < DateTime.UtcNow)
                .ToListAsync(); // AsNoTracking YOK — update edeceðiz
        }
    }
}