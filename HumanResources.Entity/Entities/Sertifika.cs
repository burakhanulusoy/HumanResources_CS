using HumanResources.Entity.Entities.Common;
using HumanResources.Entity.Enums;
using System;

namespace HumanResources.Entity.Entities
{
    public class Sertifika : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int SertifikaTuruId { get; set; }
        public SertifikaTuru SertifikaTuru { get; set; }

        public string VerenKurum { get; set; }   // Örn: Þirket Ýçi, Kýzýlay, MEB
        public string BelgeNo { get; set; }      // Belgenin resmi numarasý

        public DateTime AlinmaTarihi { get; set; }
        public DateTime GecerlilikTarihi { get; set; }
        public DateTime YenilemeTarihi { get; set; }

        public CertificateStatus Durumu { get; set; }
    }
}