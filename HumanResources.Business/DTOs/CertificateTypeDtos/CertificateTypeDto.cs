using HumanResources.Business.Base;
using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.Entity.Entities;

namespace HumanResources.Business.DTOs.CertificateTypeDtos
{
    public class CertificateTypeDto:BaseDto
    {
        public string Ad { get; set; }
        public string? Aciklama { get; set; }

        public IList<CertificateDto> Sertifikalar { get; set; }
    }
}
