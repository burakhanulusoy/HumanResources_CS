using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.UserServices
{
    public interface IUserService:IGenericService<UserDto,CreateUserDto,UpdateUserDto>
    {
        Task<BaseResult<List<ResultUserDto>>> GetAllUserWithDepartmentAndUnitAsync();
        Task<BaseResult<ResultUserDto>> GetUserWithDepartmentAndUnitAsync(int id);

    }
}
