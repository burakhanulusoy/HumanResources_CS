using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.WebUI.DTOs.UserEducationDtos;

namespace HumanResources.WebUI.DTOs.EducationDtos
{
    public class EducationDto:BaseDto
    {
        public string Ad { get; set; } // Örn: IPC J-STD-001, ESD
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }
        public TrainingStatus Durumu { get; set; }

        // Ýliþki: Bu eðitime baþvuran/katýlan personeller
        public IList<GetWithUserInfoDto> Katilimcilar { get; set; }
    }
}
