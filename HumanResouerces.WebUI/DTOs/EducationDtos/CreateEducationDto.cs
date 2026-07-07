namespace HumanResources.WebUI.DTOs.EducationDtos
{
    public class CreateEducationDto
    {
        public string Ad { get; set; } // Örn: IPC J-STD-001, ESD
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }
    }
}
