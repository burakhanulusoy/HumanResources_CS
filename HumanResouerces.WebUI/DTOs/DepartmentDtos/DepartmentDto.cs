using HumanResouerces.WebUI.Base;

namespace HumanResources.WebUI.DTOs.DepartmentDtos
{
    public class DepartmentDto:BaseDto
    {
        public string Ad { get; set; }
        public int YoneticiId { get; set; }

        public string? YoneticiAdSoyad { get; set; }
    }
}
