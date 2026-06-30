using HumanResources.Business.DTOs.ShiftDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.ShiftServices
{
    public interface IShiftService:IGenericService<ResultShiftDto,CreateShiftDto,UpdateShiftDto>
    {
    }
}
