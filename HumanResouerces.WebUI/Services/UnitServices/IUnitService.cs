using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.DTOs.UnitDtos;
using HumanResources.WebUI.DTOs.UnitDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.UnitServices
{
    public interface IUnitService : IGenericService<ResultUnitDto, CreateUnitDto, UpdateUnitDto>
    {
        Task<BaseResult<UnitWithUserDto>> GetUnitWithUsersAsync(int unitId);

    }
}