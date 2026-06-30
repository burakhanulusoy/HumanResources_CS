using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PermissionDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.PermissionServices
{
    public interface IPermissionService:IGenericService<PermissionDto,CreatePermissionDto,UpdatePermissionDto>
    {
        Task<BaseResult<List<ResultPermissionDto>>> GetAllPermissionWithUser();
        Task<BaseResult<ResultPermissionDto>> GetPermissionWithUser(int id);


    }
}
