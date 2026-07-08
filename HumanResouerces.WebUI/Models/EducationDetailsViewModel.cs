using HumanResources.WebUI.DTOs.EducationDtos;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResouerces.WebUI.Areas.Admin.Models
{
    public class EducationDetailsViewModel
    {
        public EducationDto Egitim { get; set; } = new();

        // Eğitime eklenebilir personeller (aktif kaydı olmayanlar)
        public List<UserDto> EklenebilirKullanicilar { get; set; } = new();
    }
}