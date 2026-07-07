using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.PermissionDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.PermissionServices
{
    public interface IPermissionService : IGenericService<PermissionDto, CreatePermissionDto, UpdatePermissionDto>
    {
        Task<BaseResult<List<ResultPermissionDto>>> GetAllPermissionWithUserAsync();
        Task<BaseResult<ResultPermissionDto>> GetPermissionWithUserAsync(int id);

        Task<BaseResult<List<ResultPermissionDto>>> GetByUserIdAsync(int userId);
        Task<BaseResult<List<ResultPermissionDto>>> GetMyTeamPendingPermissionsAsync(int amirId);
        Task<BaseResult<List<ResultPermissionDto>>> GetIkPendingPermissionsAsync();

        // Onaylama Metotları
        Task<BaseResult<object>> ApproveByAmirAsync(ApprovePermissionDto approveDto);
        Task<BaseResult<object>> ApproveByIkAsync(ApprovePermissionDto approveDto);
    }
}