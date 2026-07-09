using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.DemirbasDtos;
namespace HumanResources.WebUI.Services.DemirbasServices
{
    public class DemirbasService(HttpClient _client) : IDemirbasService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDemirbasDto createDto)
        {
            var response = await _client.PostAsJsonAsync("demirbaslar", createDto);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"demirbaslar/{id}");
            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultDemirbasDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultDemirbasDto>>>("demirbaslar");
        }

        public async Task<BaseResult<UpdateDemirbasDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateDemirbasDto>>($"demirbaslar/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDemirbasDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("demirbaslar", updateDto);
            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();
            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel ---
        public async Task<BaseResult<List<DemirbasDto>>> GetAllWithTypeAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<DemirbasDto>>>("demirbaslar/GetAllWithType");
        }

        public async Task<BaseResult<List<ResultDemirbasDto>>> GetAvailableAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultDemirbasDto>>>("demirbaslar/GetAvailable");
        }

        public async Task<BaseResult<DemirbasDto>> GetWithTypeAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<DemirbasDto>>($"demirbaslar/GetWithType/{id}");
        }
    }
}