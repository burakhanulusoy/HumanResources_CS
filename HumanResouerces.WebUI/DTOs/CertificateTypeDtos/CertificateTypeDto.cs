using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.CertificateDtos;

namespace HumanResources.Business.DTOs.CertificateTypeDtos
{
    public class CertificateTypeDto:BaseDto
    {
        public string Ad { get; set; }
        public string? Aciklama { get; set; }

        public IList<CertificateDto> Sertifikalar { get; set; }
    }
}
