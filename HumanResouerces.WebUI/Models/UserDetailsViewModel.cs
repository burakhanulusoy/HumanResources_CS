using HumanResources.WebUI.DTOs.UserDtos;
using HumanResources.WebUI.DTOs.UserEducationDtos;

namespace HumanResouerces.WebUI.Areas.Admin.Models
{
    public class UserDetailsViewModel
    {
        public ResultUserDto Kullanici { get; set; }

        // Kişinin başvurduğu/aldığı eğitimler (yoksa boş liste)
        public List<GetWithEducationInfoDto> Egitimler { get; set; } = new();
    }
}