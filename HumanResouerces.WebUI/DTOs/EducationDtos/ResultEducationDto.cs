using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;

namespace HumanResources.Business.DTOs.EducationDtos
{
    public class ResultEducationDto:BaseDto
    {
        public string Ad { get; set; } // Örn: IPC J-STD-001, ESD
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }
        public TrainingStatus Durumu { get; set; }



    }
}
