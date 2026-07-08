using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.Business.Services.GenericServices;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.Services.UserEducationServices
{
    public interface IUserEducationService : IGenericService<ResultUserEducationDto, CreateUserEducationDto, UpdateUserEducationDto>
    {
        Task<BaseResult<List<UserEducationDto>>> GetApplicationStatusAsync(ApplicationStatus durum);

        Task<BaseResult<List<GetWithEducationInfoDto>>> GetEducationByUserIdAsync(int userId);

        Task<BaseResult<List<GetWithUserInfoDto>>> GetUsersByEducationIdAsync(int egitimId);

        Task<BaseResult<List<UserEducationDto>>> GetUserEducationWithAllInfoAsync();

        Task<BaseResult<object>> AddParticipantByAdminAsync(CreateUserEducationDto createDto);



    }
}