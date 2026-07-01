using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.DataAccess.Repositories.UserEducationRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.UserEducationServices
{
    public class UserEducationService(IUserEducationRepository _userEducationRepository,
                                      IUnitOfWork _unitOfWork,
                                      IValidator<UpdateUserEducationDto> _updateValidator,
                                      IValidator<CreateUserEducationDto> _createValidator) : IUserEducationService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateUserEducationDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = createDto.Adapt<AppUserEgitim>();

            entity.BasvuruTarihi = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);

            entity.BasvuruDurumu = ApplicationStatus.Bekliyor;

            await _userEducationRepository.CreateAsync(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _userEducationRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("Application Not Found");
            }

            _userEducationRepository.Delete(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultUserEducationDto>>> GetAllAsync()
        {
            var entities = await _userEducationRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultUserEducationDto>>();
            return BaseResult<List<ResultUserEducationDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<UserEducationDto>>> GetApplicationStatusAsync(ApplicationStatus durum)
        {
            var entities = await _userEducationRepository.GetApplicationStatusAsync(durum);
            var mappedEntities = entities.Adapt<List<UserEducationDto>>();
            return BaseResult<List<UserEducationDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultUserEducationDto>> GetByIdAsync(int id)
        {
            var entity = await _userEducationRepository.GetByIdAsync(id);

            if (entity is null) return BaseResult<ResultUserEducationDto>.Fail("Application Not Found");

            var mappedEntity = entity.Adapt<ResultUserEducationDto>();
            return BaseResult<ResultUserEducationDto>.Success(mappedEntity);
        }

        // Profil Sayfasý: Personelin Baþvurduðu Eðitimler
        public async Task<BaseResult<List<GetWithEducationInfoDto>>> GetEducationByUserIdAsync(int userId)
        {
            var entities = await _userEducationRepository.GetEducationByUserIdAsync(userId);
            var mappedEntities = entities.Adapt<List<GetWithEducationInfoDto>>();
            return BaseResult<List<GetWithEducationInfoDto>>.Success(mappedEntities);
        }

        // Admin Paneli: Eðitime Baþvuran Personeller
        public async Task<BaseResult<List<GetWithUserInfoDto>>> GetUsersByEducationIdAsync(int egitimId)
        {
            var entities = await _userEducationRepository.GetUsersByEducationIdAsync(egitimId);
            var mappedEntities = entities.Adapt<List<GetWithUserInfoDto>>();
            return BaseResult<List<GetWithUserInfoDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<UserEducationDto>>> GetUserEducationWithAllInfoAsync()
        {
            var entities = await _userEducationRepository.GetUserEducationWithAllInfoAsync();
            var mappedEntities = entities.Adapt<List<UserEducationDto>>();
            return BaseResult<List<UserEducationDto>>.Success(mappedEntities);
        }



        //ADMÝN YAPCAK SADECE UNUTMA 
        public async Task<BaseResult<object>> UpdateAsync(UpdateUserEducationDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _userEducationRepository.GetByIdAsync(updateDto.Id);

            if (entity == null) return BaseResult<object>.Fail("Güncellenecek baþvuru bulunamadý.");

            entity.BasvuruDurumu = updateDto.BasvuruDurumu;
            entity.AdminAciklamasi = updateDto.AdminAciklamasi;

            _userEducationRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }
    }
}