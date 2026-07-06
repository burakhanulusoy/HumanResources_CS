using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;

namespace HumanResources.Business.DTOs.CertificateDtos
{
    public class ResultCertificateDto : BaseDto
    {
        public int AppUserId { get; set; }
        public int SertifikaTuruId { get; set; }

        public string VerenKurum { get; set; }
        public string BelgeNo { get; set; }

        public string? Aciklama { get; set; }

        public string? DosyaYolu { get; set; }

        public DateTime AlinmaTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime YenilemeTarihi { get; set; }
        public CertificateStatus Durumu { get; set; }
    }
}