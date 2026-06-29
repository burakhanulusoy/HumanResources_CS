using HumanResources.Business.Base;

namespace HumanResources.Business.Services.GenericServices
{
    public interface IGenericService<TResultDto, TCreateDto, TUpdateDto> where TResultDto : class
                                                                         where TCreateDto : class
                                                                         where TUpdateDto : class

    {

        Task<BaseResult<List<TResultDto>>> GetAllAsync();
        Task<BaseResult<TResultDto>> GetByIdAsync(int id);
        Task<BaseResult<object>> CreateAsync(TCreateDto createDto);
        Task<BaseResult<object>> UpdateAsync(TUpdateDto updateDto);
        Task<BaseResult<object>> DeleteAsync(int id);







    }
}
