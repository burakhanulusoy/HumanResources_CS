using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.EducationDtos
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
