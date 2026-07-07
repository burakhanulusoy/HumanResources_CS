using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Exceptions;
using HumanResources.Business.DTOs.PermissionTypeDtos;
using HumanResources.WebUI.DTOs.PermissionTypeDtos;

namespace HumanResources.WebUI.Services.PermissionTypeServices
{
    public class PermissionTypeService(HttpClient _client) : IPermissionTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreatePermissionTypeDto createDto)
        {
            var response = await _client.PostAsJsonAsync("permissiontypes", createDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var response = await _client.DeleteAsync($"permissiontypes/{id}");

            return await response.Content.ReadFromJsonAsync<BaseResult<object>>();
        }

        public async Task<BaseResult<List<ResultPermissionTypeDto>>> GetAllAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<ResultPermissionTypeDto>>>("permissiontypes");
        }

        public async Task<BaseResult<UpdatePermissionTypeDto>> GetByIdAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<UpdatePermissionTypeDto>>($"permissiontypes/{id}");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdatePermissionTypeDto updateDto)
        {
            var response = await _client.PutAsJsonAsync("permissiontypes", updateDto);

            var result = await response.Content.ReadFromJsonAsync<BaseResult<object>>();

            return result.IsFailure ? throw new ApiValidationException(result.Errors) : result;
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<PermissionTypeDto>>> GetAllWithPermissionsAsync()
        {
            return await _client.GetFromJsonAsync<BaseResult<List<PermissionTypeDto>>>("permissiontypes/GetAllWithPermissions");
        }

        public async Task<BaseResult<PermissionTypeDto>> GetWithPermissionsAsync(int id)
        {
            return await _client.GetFromJsonAsync<BaseResult<PermissionTypeDto>>($"permissiontypes/GetWithPermissions/{id}");
        }
    }
}