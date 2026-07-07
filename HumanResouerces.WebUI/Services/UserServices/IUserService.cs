using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.UserDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.UserServices
{
    public interface IUserService : IGenericService<UserDto, CreateUserDto, UpdateUserDto>
    {
        Task<BaseResult<List<ResultUserDto>>> GetAllWithDepartmentAndUnitAsync();
        Task<BaseResult<ResultUserDto>> GetByIdWithDepartmentAndUnitAsync(int id);
        Task<BaseResult<List<ResultUserDto>>> GetSubordinatesAsync(int amirId);

        Task<BaseResult<List<UserDto>>> GetUsersByRoleAsync(string roleName);
        Task<BaseResult<List<ResultUserDto>>> GetUsersByUnitIdAsync(int unitId);
        Task<BaseResult<List<ResultUserDto>>> GetAllUsersWithRolesAsync();
        Task<BaseResult<ResultUserDto>> LoginUserAsync(LoginUserDto loginUserDto);
    }
}