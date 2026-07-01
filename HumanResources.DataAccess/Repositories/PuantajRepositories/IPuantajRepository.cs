using HumanResources.Entity.Entities;

namespace HumanResources.DataAccess.Repositories.PuantajRepositories
{
    public interface IPuantajRepository : IGenericRepository<Puantaj>
    {
        // 1. Mevcut olan: İK'nın tüm listeyi detaylı görmesi için
        Task<List<Puantaj>> GetAllPuantajWithUserAndShiftAsync();



        // 2. KART OKUTMA / KONTROL İÇİN (En Kritiği)
        // Kart basıldığında "Bu adamın bugün kaydı var mı?" diye kontrol edeceğimiz metot.
        Task<Puantaj?> GetPuantajByUserIdAndDateAsync(int userId, DateTime date);




        // 3. PERSONELİN KENDİ EKRANI İÇİN
        // Personel sisteme girdiğinde sadece "Kendi" geçmiş puantajlarını görebilsin.
        Task<List<Puantaj>> GetPuantajsByUserIdAsync(int userId);



        // 4. MAAŞ VE AYLIK RAPORLAMA İÇİN
        // İK "Ahmet'in Haziran ayı (01.06 - 30.06) puantajlarını getir" dediğinde kullanılacak.
        Task<List<Puantaj>> GetPuantajsByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate);





        // 5. DEVAMSIZLARI BULMAK İÇİN (İsteğe Bağlı)
        // Gece çalışan arka plan görevinin (Background Service) o gün gelmeyenleri bulup işlem yapması için.
        Task<List<Puantaj>> GetAbsentPuantajsByDateAsync(DateTime date);





    }
}