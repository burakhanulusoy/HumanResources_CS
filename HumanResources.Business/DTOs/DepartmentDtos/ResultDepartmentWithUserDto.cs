using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.DepartmentDtos
{
    public class ResultDepartmentWithUserDto:BaseDto
    {
        public string Ad { get; set; }
        public int YoneticiId { get; set; }
        public UserDto Yonetici { get; set; }
    }
}
