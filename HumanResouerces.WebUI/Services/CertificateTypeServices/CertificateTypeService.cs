using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.CertificateTypeDtos;

namespace HumanResources.WebUI.Services.CertificateTypeServices
{
    public class CertificateTypeService(HttpClient _client) : ICertificateTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateCertificateTypeDto createDto)
        {
            var response = await _client.PostAsJsonAsync("certificatetypes", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"certificatetypes/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultCertificateTypeDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultCertificateTypeDto>>>("certificatetypes");
        }

        public async Task<BaseResult<UpdateCertificateTypeDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateCertificateTypeDto>>($"certificatetypes/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateCertificateTypeDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("certificatetypes", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<CertificateTypeDto>>> GetAllWithCertificatesAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<CertificateTypeDto>>>("certificatetypes/GetAllWithCertificates");
        }

        public async Task<BaseResult<CertificateTypeDto>> GetWithCertificatesAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<CertificateTypeDto>>($"certificatetypes/GetWithCertificates/{id}");
        }
    }
}