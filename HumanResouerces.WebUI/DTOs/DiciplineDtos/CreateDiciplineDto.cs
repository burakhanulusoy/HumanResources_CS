namespace HumanResources.WebUI.DTOs.DiciplineDtos
{
    public class CreateDiciplineDto
    {
        public int AppUserId { get; set; }
        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }

        // Kullanýcýdan dosyayý almak için:
        public IFormFile? Dosya { get; set; }
    }
}