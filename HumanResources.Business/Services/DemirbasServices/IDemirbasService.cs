using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DemirbasDtos;
using HumanResources.Business.Services.GenericServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanResources.Business.Services.DemirbasServices
{
    public interface IDemirbasService : IGenericService<ResultDemirbasDto, CreateDemirbasDto, UpdateDemirbasDto>
    {
        Task<BaseResult<List<DemirbasDto>>> GetAllWithTypeAsync();
        Task<BaseResult<List<ResultDemirbasDto>>> GetAvailableAsync();
        Task<BaseResult<DemirbasDto>> GetWithTypeByIdAsync(int id);
    }
}