using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResources.WebUI.DTOs.DiciplineDtos
{
    public class DiciplineDto:BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }
        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }
        public string? DosyaYolu { get; set; }
        public string? IspatGorseliYolu { get; set; }
        public string? TanikAdSoyad { get; set; }
    }
}
