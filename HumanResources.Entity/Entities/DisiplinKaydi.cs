using HumanResources.Entity.Entities.Common;
using System;

namespace HumanResources.Entity.Entities
{
    public class DisiplinKaydi : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // Örn: "Üst üste iþe geç kalma", "Tutanak: Ýzinsiz iþ yeri terki", "Ödül: Yýlýn Personeli"
        public string DisiplinNedeni { get; set; }

        // 3. Olayýn tam açýklamasý
        public string Detay { get; set; }

        // 4. Olayýn yaþandýðý veya ödülün hak edildiði tarih
        public DateTime OlayTarihi { get; set; }

        // 5. Belge (Islak imzalý tutanak, savunma metni veya ödül sertifikasý vb.)
        public string? DosyaYolu { get; set; }


        // YENÝ
        public string? IspatGorseliYolu { get; set; }   // Olayýn ispat görseli
        public string? TanikAdSoyad { get; set; }       // Tutanakta 1. tanýk (varsa)

    }
}