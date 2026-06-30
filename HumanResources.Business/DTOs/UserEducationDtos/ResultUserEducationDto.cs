using HumanResources.Business.Base;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.UserEducationDtos
{
    public class ResultUserEducationDto:BaseDto
    {
        public int AppUserId { get; set; }
        public int EgitimId { get; set; }
        public DateTime BasvuruTarihi { get; set; } // Baþvurunun tam saati önemli olabilir
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?
        // Eðitim Tamamlandýktan Sonrasý Ýçin
        public DateTime? SonGecerlilikTarihi { get; set; } // Örn: 2 yýl sonra biter
        public DateTime? YenilemeTarihi { get; set; } // Yenileme eðitimi ne zaman alýnmalý?
    }
}
