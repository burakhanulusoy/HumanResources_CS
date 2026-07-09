// DemirbasDto.cs  (detaylı, tür nested)
using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.WebUI.DTOs.ItemTypeDtos;
namespace HumanResources.WebUI.DTOs.DemirbasDtos
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