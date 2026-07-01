    using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;
using System;

namespace HumanResources.Entity.Entities
{
    public class Zimmet : BaseEntity
    {
        // 1. Kime verildi?
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // 2. Ne verildi? (Katalogdan seçiliyor)
        public int ZimmetTuruId { get; set; }
        public ZimmetTuru ZimmetTuru { get; set; }

        // Seri numarasý zorunlu deðil (Çünkü "Ýþ Kýyafeti"nin veya "Anahtar"ýn seri numarasý olmaz)
        public string? SeriNumarasi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        // Ýade tarihi zorunlu deðil (Çünkü adam hala kullanýyor olabilir)
        public DateTime IadeTarihi { get; set; }

        public ZimmetDurumu Durumu { get; set; }

        // Açýklama zorunlu deðil ama "Ekranýnda çizik var öyle teslim ettim" gibi notlar için hayat kurtarýr
        public string Aciklama { get; set; }
    }
}