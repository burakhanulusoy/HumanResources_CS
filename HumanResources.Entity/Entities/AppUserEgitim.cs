using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;

namespace HumanResources.Entity.Entities
{
    public class AppUserEgitim : BaseEntity
    {

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int EgitimId { get; set; }
        public Egitim Egitim { get; set; }

        public DateTime BasvuruTarihi { get; set; } // Baþvurunun tam saati önemli olabilir
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?

        // Eðitim Tamamlandýktan Sonrasý Ýçin
        public DateTime? SonGecerlilikTarihi { get; set; } // Örn: 2 yýl sonra biter
        public DateTime? YenilemeTarihi { get; set; } // Yenileme eðitimi ne zaman alýnmalý?

       
    }
}