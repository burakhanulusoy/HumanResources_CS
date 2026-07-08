using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemTypeDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.ItemDtos
{
    public class ItemDto:BaseDto
    {
    
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        // 2. Ne verildi? (Katalogdan seçiliyor)
        public int ZimmetTuruId { get; set; }
        public ResultItemTypeDto ZimmetTuru { get; set; }

        // Seri numarasý zorunlu deðil (Çünkü "Ýþ Kýyafeti"nin veya "Anahtar"ýn seri numarasý olmaz)
        public string? SeriNumarasi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        // Ýade tarihi zorunlu deðil (Çünkü adam hala kullanýyor olabilir)
        public DateTime? IadeTarihi { get; set; }

        public ZimmetDurumu Durumu { get; set; }

        // Açýklama zorunlu deðil ama "Ekranýnda çizik var öyle teslim ettim" gibi notlar için hayat kurtarýr
        public string? Aciklama { get; set; }

        public string? Marka { get; set; }
        public string? Model { get; set; }


    }
}
