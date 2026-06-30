using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.DepartmentDtos
{
    public class ResultDepartmentDto : BaseDto
    {
        public string Ad { get; set; }
        public int YoneticiId { get; set; } 
        public string YoneticiAdSoyad { get; set; }
        public int BirimlerCount { get; set; }
        public int PersonellerCount { get; set; }
    }
}