using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.UserServices
{
    public interface IUserService : IGenericService<UserDto, CreateUserDto, UpdateUserDto>
    {
        Task<BaseResult<List<ResultUserDto>>> GetAllWithDepartmentAndUnitAsync();
        Task<BaseResult<ResultUserDto>> GetByIdWithDepartmentAndUnitAsync(int id);
        Task<BaseResult<List<ResultUserDto>>> GetSubordinatesAsync(int amirId);
    }
}