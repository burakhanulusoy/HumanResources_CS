using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PermissionDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.PermissionServices
{
    public interface IPermissionService:IGenericService<PermissionDto,CreatePermissionDto,UpdatePermissionDto>
    {
        Task<BaseResult<List<ResultPermissionDto>>> GetAllPermissionWithUser();
        Task<BaseResult<ResultPermissionDto>> GetPermissionWithUser(int id);

        Task<BaseResult<List<ResultPermissionDto>>> GetMyTeamPendingPermissionsAsync(int amirId);
        Task<BaseResult<List<ResultPermissionDto>>> GetIkPendingPermissionsAsync();

        // Onaylama Metotları
        Task<BaseResult<object>> ApproveByAmirAsync(ApprovePermissionDto approveDto);
        Task<BaseResult<object>> ApproveByIkAsync(ApprovePermissionDto approveDto);
    }
}
