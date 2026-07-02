using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.ItemDtos
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }

        public int ZimmetTuruId { get; set; }

        public string? SeriNumarasi { get; set; }

        public DateTime TeslimTarihi { get; set; }

        public DateTime? IadeTarihi { get; set; }

        public ZimmetDurumu Durumu { get; set; }

        public string? Aciklama { get; set; }
    }
}
