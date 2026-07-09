// ItemDto.cs  (detay/liste - Demirbas nested)
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DemirbasDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Enums;
namespace HumanResources.Business.DTOs.ItemDtos
{
    public class ItemDto : BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int DemirbasId { get; set; }
        public DemirbasDto Demirbas { get; set; }   // marka/model/seri/tür buradan geliyor

        public DateTime TeslimTarihi { get; set; }
        public DateTime? IadeTarihi { get; set; }
        public bool SuresizMi { get; set; }
        public ZimmetDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}