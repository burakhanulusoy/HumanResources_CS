// DemirbasDto.cs  (detaylý - tür bilgisi nested; ItemDto içinde de bunu kullanacaðýz)
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemTypeDtos;
using HumanResources.Entity.Enums;
namespace HumanResources.Business.DTOs.DemirbasDtos
{
    public class DemirbasDto : BaseDto
    {
        public int ZimmetTuruId { get; set; }
        public ResultItemTypeDto ZimmetTuru { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string? SeriNumarasi { get; set; }
        public string? Aciklama { get; set; }
        public DemirbasDurumu Durumu { get; set; }
    }
}