using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Entities;

namespace HumanResources.Business.DTOs.UnitDtos
{
    public class UnitWithUserDto : BaseDto
    {
        public string Ad { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAd { get; set; }
        public string YoneticiAdSoyad { get; set; }        // <-- YENÝ
        public IList<UnitUserDto> Personeller { get; set; }
    }


    public class UnitUserDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string SicilNo { get; set; }
        public string Email { get; set; }
        public string PersonelTipi { get; set; }
        public string CalismaDurumu { get; set; }
    }
}
