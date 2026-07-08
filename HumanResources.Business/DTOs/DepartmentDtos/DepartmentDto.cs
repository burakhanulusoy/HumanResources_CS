using HumanResources.Business.Base;

namespace HumanResources.Business.DTOs.DepartmentDtos
{
    public class DepartmentDto:BaseDto
    {
        public string Ad { get; set; }
        public int YoneticiId { get; set; }
        public string? YoneticiAdSoyad { get; set; }
    }
}
