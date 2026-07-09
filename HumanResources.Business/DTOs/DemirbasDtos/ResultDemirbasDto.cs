// ResultDemirbasDto.cs  (düz liste + ItemTypeDto içinde kullanýlýyor)
using HumanResources.Business.Base;
using HumanResources.Entity.Enums;
namespace HumanResources.Business.DTOs.DemirbasDtos
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