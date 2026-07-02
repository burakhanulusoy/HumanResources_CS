namespace HumanResources.Business.DTOs.ShiftDtos
{
    public class UpdateShiftDto
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }
        public int AraDinlenmeSuresiDk { get; set; }
        public List<int>? PersonelIds { get; set; }
    }
}
