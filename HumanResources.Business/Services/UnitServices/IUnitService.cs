using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UnitDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.UnitServices
{
    public interface IUnitService : IGenericService<ResultUnitDto, CreateUnitDto, UpdateUnitDto>
    {
        
        Task<BaseResult<UnitWithUserDto>> GetUnitWithUsersAsync(int unitId);
    }
    
}
