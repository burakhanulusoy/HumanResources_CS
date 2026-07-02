namespace HumanResources.Business.DTOs.PermissionDtos
{
    public class CreatePermissionDto
    {
        public int PersonelId { get; set; }
        public int IzinTuruId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}
