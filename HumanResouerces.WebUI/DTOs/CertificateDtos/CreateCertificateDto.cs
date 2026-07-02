namespace HumanResources.Business.DTOs.CertificateDtos
{
    public class CreateCertificateDto
    {
        public int AppUserId { get; set; }
        public int SertifikaTuruId { get; set; }

        public string VerenKurum { get; set; }
        public string BelgeNo { get; set; }

        public string? Aciklama { get; set; }

        public IFormFile? Dosya { get; set; }

        public DateTime AlinmaTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime YenilemeTarihi { get; set; }
    }
}