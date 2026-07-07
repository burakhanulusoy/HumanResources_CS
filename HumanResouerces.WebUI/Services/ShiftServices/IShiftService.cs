using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.ShiftDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.ShiftServices
{
    public interface IShiftService : IGenericService<ResultShiftDto, CreateShiftDto, UpdateShiftDto>
    {
        Task<BaseResult<ResultShiftDto>> GetById2Async(int id);
    }
}