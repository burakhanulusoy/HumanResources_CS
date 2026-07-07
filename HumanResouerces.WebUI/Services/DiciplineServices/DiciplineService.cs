using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.DiciplineDtos;
using System.Globalization;
using System.Net.Http.Headers;

namespace HumanResources.WebUI.Services.DiciplineServices
{
    public class DiciplineService(HttpClient _client) : IDiciplineService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDiciplineDto createDto)
        {
            using var content = BuildFormContent(
                dosya: createDto.Dosya,
                ("AppUserId", createDto.AppUserId.ToString()),
                ("DisiplinNedeni", createDto.DisiplinNedeni),   // <-- eksikti, validation bu yüzden patlýyordu
                ("Detay", createDto.Detay),                     // <-- eksikti
                ("OlayTarihi", createDto.OlayTarihi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            );

            var response = await _client.PostAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDiciplineDto updateDto)
        {
            using var content = BuildFormContent(
                dosya: updateDto.Dosya,
                ("Id", updateDto.Id.ToString()),
                ("AppUserId", updateDto.AppUserId.ToString()),
                ("DisiplinNedeni", updateDto.DisiplinNedeni),   // <-- eksikti
                ("Detay", updateDto.Detay),                     // <-- eksikti
                ("OlayTarihi", updateDto.OlayTarihi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            );

            var response = await _client.PutAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"diciplines/{id}");
            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultDiciplineDto>>> GetAllAsync()
            => await _client.GetFromJsonAsync<BaseResult<List<ResultDiciplineDto>>>("diciplines");

        public async Task<BaseResult<UpdateDiciplineDto>> GetByIdAsync(int id)
            => await _client.GetFromJsonAsync<BaseResult<UpdateDiciplineDto>>($"diciplines/{id}");

        public async Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId)
            => await _client.GetFromJsonAsync<BaseResult<List<DiciplineDto>>>($"diciplines/GetByUserId/{userId}");

        // ---- Ortak multipart builder: alan unutma derdine son ----
        private static MultipartFormDataContent BuildFormContent(IFormFile? dosya, params (string Key, string? Value)[] fields)
        {
            var content = new MultipartFormDataContent();

            foreach (var (key, value) in fields)
                content.Add(new StringContent(value ?? string.Empty), key);

            if (dosya is not null && dosya.Length > 0)
            {
                var streamContent = new StreamContent(dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(dosya.ContentType);
                content.Add(streamContent, "Dosya", dosya.FileName);
            }

            return content;
        }
    }
}
