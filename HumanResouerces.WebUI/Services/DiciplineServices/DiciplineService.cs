using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.DiciplineDtos;
using System.Net.Http.Headers;

namespace HumanResources.WebUI.Services.DiciplineServices
{
    public class DiciplineService(HttpClient _client) : IDiciplineService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDiciplineDto createDto)
        {
            using var content = new MultipartFormDataContent();

            // Dosya ekleme iþlemi
            if (createDto.Dosya is not null)
            {
                var streamContent = new StreamContent(createDto.Dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(createDto.Dosya.ContentType);
                content.Add(streamContent, "Dosya", createDto.Dosya.FileName);
            }

            // DTO içerisindeki diðer property'leri form dataya ekliyoruz
            content.Add(new StringContent(createDto.AppUserId.ToString()), "AppUserId");

            // Tarih formatýnýn yyyy-MM-dd olduðundan emin oluyoruz
            content.Add(new StringContent(createDto.OlayTarihi.ToString("yyyy-MM-dd")), "OlayTarihi");


            var response = await _client.PostAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"diciplines/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultDiciplineDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultDiciplineDto>>>("diciplines");
        }

        public async Task<BaseResult<UpdateDiciplineDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateDiciplineDto>>($"diciplines/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDiciplineDto updateDto)
        {
            using var content = new MultipartFormDataContent();

            // Dosya ekleme iþlemi (Güncellemede yeni dosya gelmiþse)
            if (updateDto.Dosya is not null)
            {
                var streamContent = new StreamContent(updateDto.Dosya.OpenReadStream());
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(updateDto.Dosya.ContentType);
                content.Add(streamContent, "Dosya", updateDto.Dosya.FileName);
            }

            content.Add(new StringContent(updateDto.Id.ToString()), "Id");
            content.Add(new StringContent(updateDto.AppUserId.ToString()), "AppUserId");

            // Tarih formatýnýn yyyy-MM-dd olduðundan emin oluyoruz
            content.Add(new StringContent(updateDto.OlayTarihi.ToString("yyyy-MM-dd")), "OlayTarihi");

            // Not: DTO'nda bulunan diðer alanlarý (örn: Aciklama, CezaTuru vb.) buraya eklemelisin.
            // content.Add(new StringContent(updateDto.Aciklama ?? ""), "Aciklama");

            var response = await _client.PutAsync("diciplines", content);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<DiciplineDto>>>($"diciplines/GetByUserId/{userId}");
        }
    }
}