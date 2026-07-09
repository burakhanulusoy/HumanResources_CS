// CreateItemDto.cs
namespace HumanResources.WebUI.DTOs.ItemDtos
{
    public class CreateItemDto
    {
        public int AppUserId { get; set; }
        public int DemirbasId { get; set; }
        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }
        public string? Aciklama { get; set; }
    }
}