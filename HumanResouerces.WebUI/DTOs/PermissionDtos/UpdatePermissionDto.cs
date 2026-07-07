namespace HumanResources.WebUI.DTOs.PermissionDtos
{
    public class UpdatePermissionDto
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public int IzinTuruId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}
