namespace HumanResources.Business.DTOs.EducationDtos
{
    // Eðitim oluþtururken ayný anda katýlýmcýlarý da seçebilmek için
    public class CreateEducationWithParticipantsDto
    {
        public string Ad { get; set; }
        public string Egitmen { get; set; }
        public string EgitimAciklamasi { get; set; }
        public DateTime EgitimTarihi { get; set; }
        public int SuresiSaat { get; set; }

        // Seçilen personel Id'leri (multi-select'ten gelecek)
        public List<int> SelectedUserIds { get; set; } = new();
    }
}