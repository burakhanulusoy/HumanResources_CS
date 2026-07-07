using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.PermissionTypeDtos;
using HumanResources.WebUI.DTOs.PermissionTypeDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.PermissionTypeServices
{
    public interface IPermissionTypeService : IGenericService<ResultPermissionTypeDto, CreatePermissionTypeDto, UpdatePermissionTypeDto>
    {
        Task<BaseResult<List<PermissionTypeDto>>> GetAllWithPermissionsAsync();
        Task<BaseResult<PermissionTypeDto>> GetWithPermissionsAsync(int id);
    }
}