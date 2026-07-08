using HumanResources.WebUI.DTOs.CertificateDtos;
using HumanResources.WebUI.DTOs.CertificateTypeDtos;
using HumanResources.WebUI.DTOs.UserDtos;

namespace HumanResouerces.WebUI.Areas.Admin.Models
{
    public class CertificateCreateViewModel
    {
        public CreateCertificateDto Sertifika { get; set; } = new();
        public List<UserDto> TumKullanicilar { get; set; } = new();
        public List<ResultCertificateTypeDto> SertifikaTurleri { get; set; } = new();
    }
}