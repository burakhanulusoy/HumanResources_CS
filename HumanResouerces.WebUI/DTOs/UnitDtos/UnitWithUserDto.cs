using HumanResouerces.WebUI.Base;

namespace HumanResouerces.WebUI.DTOs.UnitDtos
{
    public class UnitWithUserDto : BaseDto
    {
        public string Ad { get; set; }
        public int DepartmanId { get; set; }
        public string DepartmanAd { get; set; }
        public string YoneticiAdSoyad { get; set; }        // <-- YENİ
        public IList<UnitUserDto> Personeller { get; set; }
    }

    public class UnitUserDto
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string SicilNo { get; set; }
        public string Email { get; set; }
        public string PersonelTipi { get; set; }
        public string CalismaDurumu { get; set; }
    }
}