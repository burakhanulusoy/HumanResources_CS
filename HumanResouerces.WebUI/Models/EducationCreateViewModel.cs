using HumanResources.WebUI.DTOs.EducationDtos;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResouerces.WebUI.Areas.Admin.Models
{
    public class EducationCreateViewModel
    {
        public CreateEducationWithParticipantsDto Egitim { get; set; } = new();

        // Formda checkbox listesi olarak gösterilecek tüm personel
        public List<UserDto> TumKullanicilar { get; set; } = new();
    }
}