using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.CertificateDtos;
using System.Net.Http.Headers;

namespace HumanResources.WebUI.Services.CertificateServices
{
    public class CertificateService(HttpClient _client) : ICertificateService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateCertificateDto createDto)
        {
            using var content = new MultipartFormDataContent();

            if (createDto.Dosya is not null)
            {
                var streamContent = new StreamContent(createDto.Dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(createDto.Dosya.ContentType);
                content.Add(streamContent, "Dosya", createDto.Dosya.FileName);
            }
            content.Add(new StringContent(createDto.SuresizGecerli.ToString()), "SuresizGecerli");
            content.Add(new StringContent(createDto.AppUserId.ToString()), "AppUserId");
            content.Add(new StringContent(createDto.SertifikaTuruId.ToString()), "SertifikaTuruId");
            content.Add(new StringContent(createDto.VerenKurum ?? ""), "VerenKurum");
            content.Add(new StringContent(createDto.BelgeNo ?? ""), "BelgeNo");
            content.Add(new StringContent(createDto.Aciklama ?? ""), "Aciklama");
            content.Add(new StringContent(createDto.AlinmaTarihi.ToString("yyyy-MM-dd")), "AlinmaTarihi");
            content.Add(new StringContent(createDto.GecerlilikTarihi.ToString("yyyy-MM-dd")), "GecerlilikTarihi");
            content.Add(new StringContent(createDto.YenilemeTarihi.ToString("yyyy-MM-dd")), "YenilemeTarihi");

            var response = await _client.PostAsync("certificates", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"certificates/{id}");
            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultCertificateDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultCertificateDto>>>("certificates");
        }

        public async Task<BaseResult<UpdateCertificateDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateCertificateDto>>($"certificates/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateCertificateDto updateDto)
        {
            using var content = new MultipartFormDataContent();

            if (updateDto.Dosya is not null)
            {
                var streamContent = new StreamContent(updateDto.Dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(updateDto.Dosya.ContentType);
                content.Add(streamContent, "Dosya", updateDto.Dosya.FileName);
            }

            content.Add(new StringContent(updateDto.Id.ToString()), "Id");
            content.Add(new StringContent(updateDto.AppUserId.ToString()), "AppUserId");
            content.Add(new StringContent(updateDto.SertifikaTuruId.ToString()), "SertifikaTuruId");
            content.Add(new StringContent(updateDto.VerenKurum ?? ""), "VerenKurum");
            content.Add(new StringContent(updateDto.BelgeNo ?? ""), "BelgeNo");
            content.Add(new StringContent(updateDto.Aciklama ?? ""), "Aciklama");
            content.Add(new StringContent(updateDto.AlinmaTarihi.ToString("yyyy-MM-dd")), "AlinmaTarihi");
            content.Add(new StringContent(updateDto.GecerlilikTarihi.ToString("yyyy-MM-dd")), "GecerlilikTarihi");
            content.Add(new StringContent(updateDto.YenilemeTarihi.ToString("yyyy-MM-dd")), "YenilemeTarihi");
            content.Add(new StringContent(((int)updateDto.Durumu).ToString()), "Durumu");

            var response = await _client.PutAsync("certificates", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<CertificateDto>>> GetByUserIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<CertificateDto>>>($"certificates/GetByUserId/{userId}");
        }

        public async Task<BaseResult<List<CertificateDto>>> GetUpcomingSoonAsync(int days)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<CertificateDto>>>($"certificates/GetUpcomingSoon/{days}");
        }

        public async Task<BaseResult<List<CertificateDto>>> GetByCertificateTypeIdAsync(int typeId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<CertificateDto>>>($"certificates/GetByCertificateTypeId/{typeId}");
        }

        public async Task<BaseResult<List<CertificateDto>>> GetAllWithInfoAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<CertificateDto>>>("certificates/GetAllWithInfo");
        }

        public async Task<BaseResult<CertificateDto>> GetWithInfoAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<CertificateDto>>($"certificates/GetWithInfo/{id}");
        }
    }
}