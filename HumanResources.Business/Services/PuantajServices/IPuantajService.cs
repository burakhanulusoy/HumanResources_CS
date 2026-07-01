using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PuantajDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.PuantajServices
{
    public interface IPuantajService:IGenericService<ResultPuantajDto,CreatePuantajDto,UpdatePuantajDto>
    {

        Task<BaseResult<List<PuantajDto>>> GetAllPuantajWithUserAndShiftAsync();
        Task<BaseResult<PuantajDto>> GetPuantajByUserIdAndDateAsync(int userId, DateTime date);
        Task<BaseResult<List<PuantajDto>>> GetPuantajsByUserIdAsync(int userId);
        Task<BaseResult<List<PuantajDto>>> GetPuantajsByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate);
        Task<BaseResult<List<PuantajDto>>> GetAbsentPuantajsByDateAsync(DateTime date);




    }
}
