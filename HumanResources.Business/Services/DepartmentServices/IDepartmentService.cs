using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.DepartmentServices
{
    public interface IDepartmentService:IGenericService<ResultDepartmentDto,CreateDepartmentDto,UpdateDepartmentDto>
    {
        Task<BaseResult<List<ResultDepartmentWithUserDto>>> GetDepartmentsWithUserAsync();
        Task<BaseResult<ResultDepartmentWithUserDto>> GetDepartmentWithUserAsync(int  id);
        Task<BaseResult<DepartmentUnitsDto>> GetDepartmentWithUnitsAsync(int id);


    }
}
