using System.Net.Http.Json;
using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.ShiftDtos;
using HumanResouerces.WebUI.Exceptions;

namespace HumanResources.WebUI.Services.ShiftServices
{
    public class ShiftService(HttpClient _client) : IShiftService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateShiftDto createDto)
        {
            var response = await _client.PostAsJsonAsync("shifts", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"shifts/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultShiftDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultShiftDto>>>("shifts");
        }

        public async Task<BaseResult<ResultShiftDto>> GetById2Async(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<ResultShiftDto>>($"shifts/{id}");
        }

        public async Task<BaseResult<UpdateShiftDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateShiftDto>>($"shifts/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateShiftDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("shifts", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

      

    }
}