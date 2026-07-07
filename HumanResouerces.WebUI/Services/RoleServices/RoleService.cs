using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.RoleDtos;

namespace HumanResources.WebUI.Services.RoleServices
{
    public class RoleService(HttpClient _client) : IRoleService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateRoleDto createDto)
        {
            var response = await _client.PostAsJsonAsync("roles", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"roles/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultRoleDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultRoleDto>>>("roles");
        }

        // DÜZELTÝLEN KISIM: ResultRoleDto yerine UpdateRoleDto kullanýldý.
        public async Task<BaseResult<UpdateRoleDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdateRoleDto>>($"roles/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateRoleDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("roles", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<AssignRoleDto>>> GetUserForRoleAssignAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<AssignRoleDto>>>($"roles/getUserRole/{id}");
        }

        public async Task<BaseResult<object>> AssignRoleAsync(List<AssignRoleDto> assignRoleDtos)
        {
            var response = await _client.PostAsJsonAsync("roles/assign", assignRoleDtos);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }
    }
}