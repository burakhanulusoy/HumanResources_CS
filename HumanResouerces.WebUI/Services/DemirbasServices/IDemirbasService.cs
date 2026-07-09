using HumanResouerces.WebUI.Base;
using HumanResources.WebUI.DTOs.DemirbasDtos;
using HumanResources.WebUI.Services.GenericServices;
namespace HumanResources.WebUI.Services.DemirbasServices
{
    public interface IDemirbasService : IGenericService<ResultDemirbasDto, CreateDemirbasDto, UpdateDemirbasDto>
    {
        Task<BaseResult<List<DemirbasDto>>> GetAllWithTypeAsync();
        Task<BaseResult<List<ResultDemirbasDto>>> GetAvailableAsync();
        Task<BaseResult<DemirbasDto>> GetWithTypeAsync(int id);
    }
}