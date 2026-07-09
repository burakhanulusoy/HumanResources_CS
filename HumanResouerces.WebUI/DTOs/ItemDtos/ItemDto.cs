// ItemDto.cs  (Demirbas nested)
using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.WebUI.DTOs.DemirbasDtos;
using HumanResources.WebUI.DTOs.UserDtos;
namespace HumanResources.WebUI.DTOs.ItemDtos
{
    public class ItemDto : BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int DemirbasId { get; set; }
        public DemirbasDto Demirbas { get; set; }

        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }
        public ZimmetDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}