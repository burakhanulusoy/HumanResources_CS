// CreateDemirbasDto.cs
namespace HumanResources.Business.DTOs.DemirbasDtos
{
    public class CreateDemirbasDto
    {
        public int ZimmetTuruId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public string? SeriNumarasi { get; set; }
        public string? Aciklama { get; set; }
    }
}