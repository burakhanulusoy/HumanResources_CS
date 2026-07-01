using HumanResources.DataAccess.Context;
using HumanResources.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.DataAccess.Repositories.PuantajRepositories
{
    public class PuantajRepository : GenericRepository<Puantaj>, IPuantajRepository
    {
        public PuantajRepository(AppDbContext _context) : base(_context)
        {
        }

        // 1. Mevcut olan: ÝK'nýn tüm listeyi detaylý görmesi için
        public async Task<List<Puantaj>> GetAllPuantajWithUserAndShiftAsync()
        {
            return await _table
                .Include(x => x.AppUser)  // Personel bilgileri (Ad, Soyad vb.)
                .Include(x => x.Vardiya)  // Vardiya detaylarý gelsin
                .OrderByDescending(x => x.Tarih) // En güncel kayýtlar en üstte görünsün
                .AsNoTracking()
                .ToListAsync();
        }

        // 2. KART OKUTMA / KONTROL ÝÇÝN (En Kritiði)
        public async Task<Puantaj?> GetPuantajByUserIdAndDateAsync(int userId, DateTime date)
        {
            // Bu metot genellikle iþlem (Update/Hesaplama) yapmak için çaðrýlacaðý için 
            // AsNoTracking() KULLANMIYORUZ ki EF Core bu kaydý takip etsin, deðiþiklik yapabilelim.
            return await _table
                .Include(x => x.Vardiya) // Hesaplama yaparken vardiyanýn baþlangýç/bitiþ saati lazým olacak
                .FirstOrDefaultAsync(x => x.AppUserId == userId && x.Tarih.Date == date.Date);
        }

        // 3. PERSONELÝN KENDÝ EKRANI ÝÇÝN
        public async Task<List<Puantaj>> GetPuantajsByUserIdAsync(int userId)
        {
            return await _table
                .Include(x => x.Vardiya)
                .Where(x => x.AppUserId == userId)
                .OrderByDescending(x => x.Tarih) // Personel en son gününü en üstte görsün
                .AsNoTracking()
                .ToListAsync();
        }

        // 4. MAAÞ VE AYLIK RAPORLAMA ÝÇÝN
        public async Task<List<Puantaj>> GetPuantajsByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return await _table
                .Include(x => x.Vardiya)
                .Where(x => x.AppUserId == userId &&
                            x.Tarih.Date >= startDate.Date &&
                            x.Tarih.Date <= endDate.Date)
                .OrderBy(x => x.Tarih) // Aylýk raporda kronolojik sýralama (Ayýn 1'inden 30'una doðru) daha mantýklýdýr
                .AsNoTracking()
                .ToListAsync();
        }

        // 5. DEVAMSIZLARI BULMAK ÝÇÝN 
        public async Task<List<Puantaj>> GetAbsentPuantajsByDateAsync(DateTime date)
        {
            return await _table
                .Include(x => x.AppUser) // Kimin gelmediðini görmek için user lazým
                .Where(x => x.Tarih.Date == date.Date && x.Devamsiz == true)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}