using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.CertificateDtos
{
    public class UpdateCertificateDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }

        public int SertifikaTuruId { get; set; }

        public string VerenKurum { get; set; }   // Örn: Þirket Ýçi, Kýzýlay, MEB
        public string BelgeNo { get; set; }      // Belgenin resmi numarasý

        public DateTime AlinmaTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime YenilemeTarihi { get; set; }
        public CertificateStatus Durumu { get; set; }
    }
}
