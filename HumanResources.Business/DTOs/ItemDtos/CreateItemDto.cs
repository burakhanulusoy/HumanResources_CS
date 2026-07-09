// CreateItemDto.cs
namespace HumanResources.Business.DTOs.ItemDtos
{
    public class CreateItemDto
    {
        public int AppUserId { get; set; }
        public int DemirbasId { get; set; }          // ZimmetTuruId + Marka/Model/Seri gitti
        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }     // nullable
        public bool SuresizMi { get; set; }           // "sonsuz" seçeneði
        public string? Aciklama { get; set; }
    }
}