using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResources.WebUI.DTOs.UserEducationDtos
{
    public class GetWithUserInfoDto:BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int EgitimId { get; set; }

        public DateTime BasvuruTarihi { get; set; } // Baþvurunun tam saati önemli olabilir
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?

     
    }
}
