using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.DiciplineDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.DiciplineServices
{
    public interface IDiciplineService : IGenericService<ResultDiciplineDto, CreateDiciplineDto, UpdateDiciplineDto>
    {
        Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId);
    }
}