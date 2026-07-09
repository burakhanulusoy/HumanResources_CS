// UpdateDemirbasDto.cs
using HumanResources.Entity.Enums;
namespace HumanResources.Business.DTOs.DemirbasDtos
{
    public class UpdateDemirbasDto
    {
        public int Id { get; set; }
        public int ZimmetTuruId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string? SeriNumarasi { get; set; }
        public string? Aciklama { get; set; }
        public DemirbasDurumu Durumu { get; set; }
    }
}