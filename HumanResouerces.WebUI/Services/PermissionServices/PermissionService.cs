using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.WebUI.DTOs.PermissionDtos;

namespace HumanResources.WebUI.Services.PermissionServices
{
    public class PermissionService(HttpClient _client) : IPermissionService
    {
        public async Task<BaseResult<object>> CreateAsync(CreatePermissionDto createDto)
        {
            var response = await _client.PostAsJsonAsync("permissions", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"permissions/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<PermissionDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<PermissionDto>>>("permissions");
        }

        public async Task<BaseResult<UpdatePermissionDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdatePermissionDto>>($"permissions/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdatePermissionDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("permissions", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ResultPermissionDto>>> GetAllPermissionWithUserAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultPermissionDto>>>("permissions/GetAllWithUser");
        }

        public async Task<BaseResult<ResultPermissionDto>> GetPermissionWithUserAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<ResultPermissionDto>>($"permissions/GetWithUser/{id}");
        }

        public async Task<BaseResult<List<ResultPermissionDto>>> GetByUserIdAsync(int userId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultPermissionDto>>>($"permissions/GetByUserId/{userId}");
        }

        public async Task<BaseResult<List<ResultPermissionDto>>> GetMyTeamPendingPermissionsAsync(int amirId)
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultPermissionDto>>>($"permissions/GetMyTeamPendingPermissions/{amirId}");
        }

        public async Task<BaseResult<List<ResultPermissionDto>>> GetIkPendingPermissionsAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultPermissionDto>>>("permissions/GetIkPendingPermissions");
        }

        // --- Onaylama Metotlarý ---

        public async Task<BaseResult<object>> ApproveByAmirAsync(ApprovePermissionDto approveDto)
        {
            var response = await _client.PutAsJsonAsync("permissions/ApproveByAmir", approveDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> ApproveByIkAsync(ApprovePermissionDto approveDto)
        {
            var response = await _client.PutAsJsonAsync("permissions/ApproveByIk", approveDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }
    }
}