using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.DepartmentServices
{
    public interface IDepartmentService:IGenericService<ResultDepartmentDto,CreateDepartmentDto,UpdateDepartmentDto>
    {
    }
}
