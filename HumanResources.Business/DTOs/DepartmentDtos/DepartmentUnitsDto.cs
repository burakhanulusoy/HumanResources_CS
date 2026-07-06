using HumanResources.Business.Base;

namespace HumanResources.Business.DTOs.DepartmentDtos
{
    // Departmanýn birimleri sayfasý için
    public class DepartmentUnitsDto : BaseDto
    {
        public string Ad { get; set; }                       // Departman adý
        public string YoneticiAdSoyad { get; set; }
        public IList<DepartmentUnitItemDto> Birimler { get; set; }
    }

    public class DepartmentUnitItemDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int PersonellerCount { get; set; }   // Mapster: Personeller.Count flatten

    }
}