using HumanResouerces.WebUI.Base;
using HumanResouerces.WebUI.Enums;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.WebUI.Services.GenericServices;

namespace HumanResources.WebUI.Services.UserEducationServices
{
    public interface IUserEducationService : IGenericService<ResultUserEducationDto, CreateUserEducationDto, UpdateUserEducationDto>
    {
        Task<BaseResult<List<UserEducationDto>>> GetByStatusAsync(ApplicationStatus durum);
        Task<BaseResult<List<GetWithEducationInfoDto>>> GetByUserIdAsync(int userId);
        Task<BaseResult<List<GetWithUserInfoDto>>> GetByEducationIdAsync(int egitimId);
        Task<BaseResult<List<UserEducationDto>>> GetAllWithInfoAsync();
    }
}