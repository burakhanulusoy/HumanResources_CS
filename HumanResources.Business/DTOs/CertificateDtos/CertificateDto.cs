using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.DTOs.CertificateDtos
{
    public class CertificateDto:BaseDto
    {
        public int AppUserId { get; set; }
        public UserDto AppUser { get; set; }

        public int SertifikaTuruId { get; set; }
        public ResultCertificateTypeDto SertifikaTuru { get; set; }

        public string VerenKurum { get; set; }   // Örn: Þirket Ýçi, Kýzýlay, MEB
        public string BelgeNo { get; set; }      // Belgenin resmi numarasý

        public DateTime AlinmaTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime YenilemeTarihi { get; set; }

        public CertificateStatus Durumu { get; set; }
    }
}
