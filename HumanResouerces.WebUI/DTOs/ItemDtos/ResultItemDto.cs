using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;

namespace HumanResources.WebUI.DTOs.ItemDtos
{
    public class ResultItemDto:BaseDto
    {
        public int AppUserId { get; set; }

        public int ZimmetTuruId { get; set; }

        public string? SeriNumarasi { get; set; }
            
        public DateTime TeslimTarihi { get; set; }

        public DateTime? IadeTarihi { get; set; }

        public ZimmetDurumu Durumu { get; set; }

        public string? Aciklama { get; set; }
    }
}
