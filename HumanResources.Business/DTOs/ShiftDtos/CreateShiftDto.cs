namespace HumanResources.Business.DTOs.ShiftDtos
{
    public class CreateShiftDto
    {
        public string Aciklama { get; set; } 
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }     
        public int AraDinlenmeSuresiDk { get; set; } 
        public List<int>? PersonelIds { get; set; }
        public int? YoneticiId { get; set; }
    }
}
