using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PermissionTypeDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.PermissionTypeServices
{
    public interface IPermissionTypeService:IGenericService<ResultPermissionTypeDto,CreatePermissionTypeDto,UpdatePermissionTypeDto>
    {
        Task<BaseResult<List<PermissionTypeDto>>> GetAllPermissionTypeWithPermissions();
        Task<BaseResult<PermissionTypeDto>> GetPermissionTypeWithPermissions(int id);


    }
}
