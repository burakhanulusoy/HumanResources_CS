using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.DataAccess.Repositories.EducationRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.EducationServices
{
    public class EducationService(IEducationRepository _educationRepository,
                                  IUnitOfWork _unitOfWork,
                                  IValidator<UpdateEducationDto> _updateValidator,
                                  IValidator<CreateEducationDto> _createValidator) : IEducationService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateEducationDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = createDto.Adapt<Egitim>();

            // Ýþ Kuralý 1: PostgreSQL için tarihi UTC formatýna zorla
            entity.EgitimTarihi = DateTime.SpecifyKind(entity.EgitimTarihi, DateTimeKind.Utc);

            // Ýþ Kuralý 2: Yeni oluþturulan her eðitim varsayýlan olarak "Planlandý" statüsündedir
            entity.Durumu = TrainingStatus.Planlandi;

            await _educationRepository.CreateAsync(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _educationRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<object>.Fail("Education Not Found");
            }

            _educationRepository.Delete(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultEducationDto>>> GetAllAsync()
        {
            var entities = await _educationRepository.GetAllAsync();

            var mappedEntities = entities.Adapt<List<ResultEducationDto>>();

            return BaseResult<List<ResultEducationDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<EducationDto>>> GetAllEducationWithUsersAsync()
        {
            var entities = await _educationRepository.GetAllEducationWithUserAsync();

            var mappedEntities = entities.Adapt<List<EducationDto>>();

            return BaseResult<List<EducationDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultEducationDto>> GetByIdAsync(int id)
        {
            var entity = await _educationRepository.GetByIdAsync(id);

            if (entity is null)
            {
                return BaseResult<ResultEducationDto>.Fail("Education Not Found");
            }

            var mappedEntity = entity.Adapt<ResultEducationDto>();

            return BaseResult<ResultEducationDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<EducationDto>> GetEducationWithUsersAsync(int id)
        {
            var entity = await _educationRepository.GetEducationWithUserAsync(id);

            if (entity is null)
            {
                return BaseResult<EducationDto>.Fail("Education Not Found");
            }

            var mappedEntity = entity.Adapt<EducationDto>();

            return BaseResult<EducationDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateEducationDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var entity = await _educationRepository.GetByIdAsync(updateDto.Id);

            if (entity == null)
                return BaseResult<object>.Fail("Güncellenecek kayýt bulunamadý.");

            updateDto.Adapt(entity);

            entity.EgitimTarihi = DateTime.SpecifyKind(entity.EgitimTarihi, DateTimeKind.Utc);

            _educationRepository.Update(entity);

            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }
    }
}