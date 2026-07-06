using System.Net.Http.Json;
using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResouerces.WebUI.Exceptions;
using HumanResouerces.WebUI.Enums;

namespace HumanResources.WebUI.Services.UserEducationServices
{
    public class UserEducationService(HttpClient _client) : IUserEducationService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUserEducationDto createDto)
        {
            var response = await _client.PostAsJsonAsync("usereducations", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"usereducations/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultUserEducationDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUserEducationDto>>>("usereducations");
        }

        // Generic arayüze uygun olarak UpdateUserEducationDto dönüyoruz
        public async Task<BaseResult<UpdateUserEducationDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateUserEducationDto>>($"usereducations/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUserEducationDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("usereducations", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<UserEducationDto>>> GetByStatusAsync(ApplicationStatus durum)
        {
            // Enum deðeri URL'e int olarak ya da string olarak gidebilir. .NET Core default olarak enum adýný veya numarasýný mapleyebilir.
            return await _client.GetFromJsonAsync<BaseResult<List<UserEducationDto>>>($"usereducations/GetByStatus/{(int)durum}");
        }

        public async Task<BaseResult<List<GetWithEducationInfoDto>>> GetByUserIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<GetWithEducationInfoDto>>>($"usereducations/GetByUserId/{userId}");
        }

        public async Task<BaseResult<List<GetWithUserInfoDto>>> GetByEducationIdAsync(int egitimId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<GetWithUserInfoDto>>>($"usereducations/GetByEducationId/{egitimId}");
        }

        public async Task<BaseResult<List<UserEducationDto>>> GetAllWithInfoAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<UserEducationDto>>>("usereducations/GetAllWithInfo");
        }
    }
}