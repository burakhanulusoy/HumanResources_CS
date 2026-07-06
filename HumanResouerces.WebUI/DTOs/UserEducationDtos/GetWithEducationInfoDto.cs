using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.Business.DTOs.EducationDtos;

namespace HumanResources.Business.DTOs.UserEducationDtos
{
    public class GetWithEducationInfoDto:BaseDto
    {
        public int AppUserId { get; set; }

        public int EgitimId { get; set; }
        public ResultEducationDto Egitim { get; set; }

        public DateTime BasvuruTarihi { get; set; } // Baþvurunun tam saati önemli olabilir
        public ApplicationStatus BasvuruDurumu { get; set; }
        public string? AdminAciklamasi { get; set; } // Reddedilirse neden reddedildi?

    

    }
}
