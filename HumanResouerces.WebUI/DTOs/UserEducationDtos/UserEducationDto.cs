using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.UserEducationDtos
{
    public class UserEducationDto:BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int EgitimId { get; set; }
        public ResultEducationDto Egitim { get; set; }

        public DateTime BasvuruTarihi { get; set; } // Baþvurunun tam saati önemli olabilir
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?

        // Eðitim Tamamlandýktan Sonrasý Ýçin
     


    }
}
