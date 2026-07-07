using HumanResources.Business.Base;
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.Business.Services.GenericServices;

namespace HumanResources.Business.Services.EducationServices
{
    public interface IEducationService:IGenericService<ResultEducationDto,CreateEducationDto,UpdateEducationDto>
    {

        Task<BaseResult<List<EducationDto>>> GetAllEducationWithUsersAsync();
        Task<BaseResult<EducationDto>> GetEducationWithUsersAsync(int id);

        Task<BaseResult<object>> CreateWithParticipantsAsync(CreateEducationWithParticipantsDto dto);




    }
}
