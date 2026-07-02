using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.UnitDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.UnitServices
{
    public interface IUnitService : IGenericService<ResultUnitDto, CreateUnitDto, UpdateUnitDto>
    {
    }
}