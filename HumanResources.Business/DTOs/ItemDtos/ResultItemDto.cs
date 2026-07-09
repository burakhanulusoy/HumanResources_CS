// ResultItemDto.cs
using HumanResources.Business.Base;
using HumanResources.Entity.Enums;
namespace HumanResources.Business.DTOs.ItemDtos
{
    public class ResultItemDto : BaseDto
    {
        public int AppUserId { get; set; }
        public int DemirbasId { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }
        public ZimmetDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}