using HumanResources.Business.Base;
using HumanResources.Business.DTOs.RoleDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.RoleServices
{
    public interface IRoleService:IGenericService<ResultRoleDto,CreateRoleDto,UpdateRoleDto>
    {
        Task<BaseResult<List<AssignRoleDto>>> GetUserForRoleAssignAsync(int id);

        Task<BaseResult<object>> AssignRoleAsync(List<AssignRoleDto> assignRoleDtos);


    }
}
