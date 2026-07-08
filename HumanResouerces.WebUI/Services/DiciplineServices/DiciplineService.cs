using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.DiciplineDtos;
using System.Globalization;
using System.Net.Http.Headers;

namespace HumanResources.WebUI.Services.DiciplineServices
{
    public class DiciplineService(HttpClient _client) : IDiciplineService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDiciplineDto createDto)
        {
            using var content = BuildFormContent(
                dosyalar: new[] { ("Dosya", createDto.Dosya), ("IspatGorseli", createDto.IspatGorseli) },
                ("AppUserId", createDto.AppUserId.ToString()),
                ("DisiplinNedeni", createDto.DisiplinNedeni),
                ("Detay", createDto.Detay),
                ("TanikAdSoyad", createDto.TanikAdSoyad),
                ("OlayTarihi", createDto.OlayTarihi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            );
            var response = await _client.PostAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDiciplineDto updateDto)
        {
            using var content = BuildFormContent(
                dosyalar: new[] { ("Dosya", updateDto.Dosya), ("IspatGorseli", updateDto.IspatGorseli) },
                ("Id", updateDto.Id.ToString()),
                ("AppUserId", updateDto.AppUserId.ToString()),
                ("DisiplinNedeni", updateDto.DisiplinNedeni),
                ("Detay", updateDto.Detay),
                ("TanikAdSoyad", updateDto.TanikAdSoyad),
                ("OlayTarihi", updateDto.OlayTarihi.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
            );
            var response = await _client.PutAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        private static MultipartFormDataContent BuildFormContent(
            (string FieldName, IFormFile? File)[] dosyalar,
            params (string Key, string? Value)[] fields)
        {
            var content = new MultipartFormDataContent();
            foreach (var (key, value) in fields)
                content.Add(new StringContent(value ?? string.Empty), key);

            foreach (var (fieldName, file) in dosyalar)
            {
                if (file is not null && file.Length > 0)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                    content.Add(streamContent, fieldName, file.FileName);
                }
            }
            return content;
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

       
    }
}
