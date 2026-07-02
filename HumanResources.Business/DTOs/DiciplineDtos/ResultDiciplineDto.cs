using HumanResources.Business.Base;

namespace HumanResources.Business.DTOs.DiciplineDtos
{
    public class ResultDiciplineDto:BaseDto
    {
        public int AppUserId { get; set; }
        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }
        public string? DosyaYolu { get; set; }
    }
}
