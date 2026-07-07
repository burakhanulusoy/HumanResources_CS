using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.CertificateDtos;

namespace HumanResources.WebUI.DTOs.CertificateTypeDtos
{
    public class CertificateTypeDto:BaseDto
    {
        public string Ad { get; set; }
        public string? Aciklama { get; set; }

        public IList<CertificateDto> Sertifikalar { get; set; }
    }
}
