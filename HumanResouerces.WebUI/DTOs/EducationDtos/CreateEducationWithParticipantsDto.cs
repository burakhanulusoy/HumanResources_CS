namespace HumanResources.WebUI.DTOs.EducationDtos   
{
    public class CreateEducationWithParticipantsDto
    {
        public string Ad { get; set; }
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }
        public List<int> SelectedUserIds { get; set; } = new();
    }
}