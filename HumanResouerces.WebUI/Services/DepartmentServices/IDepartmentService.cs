using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.DTOs.DepartmentDtos;
using HumanResources.WebUI.DTOs.DepartmentDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.DepartmentServices
{
    public interface IDepartmentService : IGenericService<ResultDepartmentDto, CreateDepartmentDto, UpdateDepartmentDto>
    {
        Task<BaseResult<List<ResultDepartmentWithUserDto>>> GetDepartmentsWithUserAsync();
        Task<BaseResult<ResultDepartmentWithUserDto>> GetDepartmentWithUserAsync(int id);
        Task<BaseResult<DepartmentUnitsDto>> GetDepartmentWithUnitsAsync(int id);

    }
}