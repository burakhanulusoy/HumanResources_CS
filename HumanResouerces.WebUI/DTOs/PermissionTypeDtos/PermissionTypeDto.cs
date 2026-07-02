using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.PermissionDtos;

namespace HumanResources.Business.DTOs.PermissionTypeDtos
{
    public class PermissionTypeDto:BaseDto
    {
        public string Ad { get; set; }
        public bool UcretliMi { get; set; }
        public IList<ResultPermissionDto> Izinler { get; set; }
    }
}
