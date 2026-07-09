// UpdateItemDto.cs
using HumanResouerces.WebUI.Enums;
namespace HumanResources.WebUI.DTOs.ItemDtos
{
    public class UpdateItemDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public int DemirbasId { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }
        public ZimmetDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}