using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.RoleDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.RoleServices
{
    public interface IRoleService : IGenericService<ResultRoleDto, CreateRoleDto, UpdateRoleDto>
    {
        Task<BaseResult<List<AssignRoleDto>>> GetUserForRoleAssignAsync(int id);
        Task<BaseResult<object>> AssignRoleAsync(List<AssignRoleDto> assignRoleDtos);
    }
}