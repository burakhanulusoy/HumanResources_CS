using System.Net.Http.Json;
using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UnitDtos;
using HumanResouerces.WebUI.Exceptions;
using HumanResouerces.WebUI.DTOs.UnitDtos;

namespace HumanResources.WebUI.Services.UnitServices
{
    public class UnitService(HttpClient _client) : IUnitService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUnitDto createDto)
        {
            var response = await _client.PostAsJsonAsync("units", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"units/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultUnitDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultUnitDto>>>("units");
        }

        public async Task<BaseResult<UpdateUnitDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateUnitDto>>($"units/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateUnitDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("units", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<UnitWithUserDto>> GetUnitWithUsersAsync(int unitId)
        {
            return await _client.GetFromJsonAsync<BaseResult<UnitWithUserDto>>($"units/GetUnitWithUsers/{unitId}");
        }
    }
}