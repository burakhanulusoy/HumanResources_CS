using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResources.WebUI.DTOs.DepartmentDtos
{
    public class ResultDepartmentWithUserDto:BaseDto
    {
        public string Ad { get; set; }

        public int? YoneticiId { get; set; }

        public UserDto Yonetici { get; set; }

        public List<UserDto> Personeller { get; set; }
    }
}
