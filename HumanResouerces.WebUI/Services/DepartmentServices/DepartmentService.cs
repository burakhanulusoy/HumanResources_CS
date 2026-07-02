using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.DepartmentDtos;

namespace HumanResources.WebUI.Services.DepartmentServices
{
    public class DepartmentService(HttpClient _client) : IDepartmentService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDepartmentDto createDto)
        {
            var response = await _client.PostAsJsonAsync("departments", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"departments/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultDepartmentDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultDepartmentDto>>>("departments");
        }

        public async Task<BaseResult<UpdateDepartmentDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateDepartmentDto>>($"departments/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDepartmentDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("departments", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ResultDepartmentWithUserDto>>> GetDepartmentsWithUserAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultDepartmentWithUserDto>>>("departments/GetDepartmentsWithUser");
        }

        // URL GÜNCELLENDÝ: Route parametresi kullanýldý
        public async Task<BaseResult<ResultDepartmentWithUserDto>> GetDepartmentWithUserAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<ResultDepartmentWithUserDto>>($"departments/GetDepartmentWithUser/{id}");
        }
    }
}