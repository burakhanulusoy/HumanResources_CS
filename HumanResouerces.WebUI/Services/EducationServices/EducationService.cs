using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.EducationDtos;

namespace HumanResources.WebUI.Services.EducationServices
{
    public class EducationService(HttpClient _client) : IEducationService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateEducationDto createDto)
        {
            var response = await _client.PostAsJsonAsync("educations", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"educations/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultEducationDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultEducationDto>>>("educations");
        }

        public async Task<BaseResult<UpdateEducationDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateEducationDto>>($"educations/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateEducationDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("educations", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<EducationDto>>> GetAllWithUsersAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<EducationDto>>>("educations/GetAllWithUsers");
        }

        public async Task<BaseResult<EducationDto>> GetWithUsersAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<EducationDto>>($"educations/GetWithUsers/{id}");
        }
    }
}