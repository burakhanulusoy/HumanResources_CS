using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.DiciplineDtos
{
    public class DiciplineDto : BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }
        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }
        public string? DosyaYolu { get; set; }
        public string? IspatGorseliYolu { get; set; }
        public string? TanikAdSoyad { get; set; }
        public string? TanikAdSoyad2 { get; set; }
    }
}