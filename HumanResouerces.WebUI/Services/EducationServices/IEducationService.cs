using HumanResouerces.WebUI.Base;
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.EducationServices
{
    public interface IEducationService : IGenericService<ResultEducationDto, CreateEducationDto, UpdateEducationDto>
    {
        Task<BaseResult<List<EducationDto>>> GetAllWithUsersAsync();
        Task<BaseResult<EducationDto>> GetWithUsersAsync(int id);
    }
}