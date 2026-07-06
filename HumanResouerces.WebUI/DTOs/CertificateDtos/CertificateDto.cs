using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.DTOs.CertificateDtos
{
    public class CertificateDto : BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int SertifikaTuruId { get; set; }
        public ResultCertificateTypeDto SertifikaTuru { get; set; }

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