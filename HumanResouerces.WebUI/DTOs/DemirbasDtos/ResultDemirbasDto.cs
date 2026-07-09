// ResultDemirbasDto.cs
using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
namespace HumanResources.WebUI.DTOs.DemirbasDtos
{
    public class ResultDemirbasDto : BaseDto
    {
        public int ZimmetTuruId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string? SeriNumarasi { get; set; }
        public DemirbasDurumu Durumu { get; set; }
        public string? Aciklama { get; set; }
    }
}