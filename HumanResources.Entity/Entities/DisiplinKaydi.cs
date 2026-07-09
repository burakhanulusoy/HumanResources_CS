using HumanResources.Entity.Entities.Common;
using System;

namespace HumanResources.Entity.Entities
{
    public class DisiplinKaydi : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }
        public string? DosyaYolu { get; set; }

        public string? IspatGorseliYolu { get; set; }
        public string? TanikAdSoyad { get; set; }

        // YENÝ: Ýkinci tanýk (opsiyonel)
        public string? TanikAdSoyad2 { get; set; }
    }
}
