using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.ShiftDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.ShiftServices
{
    public interface IShiftService : IGenericService<ResultShiftDto, CreateShiftDto, UpdateShiftDto>
    {
    }
}