using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
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
                .Include(x => x.SertifikaTuru) // Bildirim atarken sertifikanýn adýný bilmek için lazým
                                               // Durumu geçerli olan ve yenileme tarihi bugünden büyük ama hedef tarihten küçük/eþit olanlar
                .Where(x => x.Durumu == Entity.Enums.CertificateStatus.Gecerli &&
                            x.YenilemeTarihi > DateTime.UtcNow &&
                            x.YenilemeTarihi <= hedefTarih)
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
    }
}