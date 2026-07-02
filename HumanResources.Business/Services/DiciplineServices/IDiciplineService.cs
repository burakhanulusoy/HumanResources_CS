using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DepartmentDtos;
using HumanResources.Business.DTOs.DiciplineDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.DiciplineServices
{
    public interface IDiciplineService:IGenericService<ResultDiciplineDto,CreateDiciplineDto,UpdateDiciplineDto>
    {
        Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId); // Personele ait kayıtlar
    }
}
