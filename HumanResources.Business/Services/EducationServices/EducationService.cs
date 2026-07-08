// Business/Services/EducationServices/EducationService.cs
using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.EducationDtos;
using HumanResources.DataAccess.Repositories.EducationRepositories;
using HumanResources.DataAccess.Repositories.UserEducationRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.EducationServices
{
    public class EducationService(IEducationRepository _educationRepository,
                                  IUserEducationRepository _userEducationRepository,
                                  IUnitOfWork _unitOfWork,
                                  IValidator<UpdateEducationDto> _updateValidator,
                                  IValidator<CreateEducationDto> _createValidator) : IEducationService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateEducationDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
                return BaseResult<object>.Fail(validationResult.Errors);

            var entity = createDto.Adapt<Egitim>();
            entity.EgitimTarihi = DateTime.SpecifyKind(entity.EgitimTarihi, DateTimeKind.Utc);

            // ÝÞ KURALI: Eðitim tarihi geçmiþte ise, eðitim zaten yapýlmýþ demektir -> otomatik "Tamamlandý"
            // Bugün veya gelecekteyse -> "Planlandý"
            entity.Durumu = entity.EgitimTarihi.Date < DateTime.UtcNow.Date
                ? TrainingStatus.Tamamlandi
                : TrainingStatus.Planlandi;

            await _educationRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _educationRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Education Not Found");

            _educationRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultEducationDto>>> GetAllAsync()
        {
            var entities = await _educationRepository.GetAllAsync();
            return BaseResult<List<ResultEducationDto>>.Success(entities.Adapt<List<ResultEducationDto>>());
        }

        public async Task<BaseResult<List<EducationDto>>> GetAllEducationWithUsersAsync()
        {
            var entities = await _educationRepository.GetAllEducationWithUserAsync();
            return BaseResult<List<EducationDto>>.Success(entities.Adapt<List<EducationDto>>());
        }

        public async Task<BaseResult<ResultEducationDto>> GetByIdAsync(int id)
        {
            var entity = await _educationRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultEducationDto>.Fail("Education Not Found");
            return BaseResult<ResultEducationDto>.Success(entity.Adapt<ResultEducationDto>());
        }

        public async Task<BaseResult<EducationDto>> GetEducationWithUsersAsync(int id)
        {
            var entity = await _educationRepository.GetEducationWithUserAsync(id);
            if (entity is null) return BaseResult<EducationDto>.Fail("Education Not Found");
            return BaseResult<EducationDto>.Success(entity.Adapt<EducationDto>());
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateEducationDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
                return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _educationRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek kayýt bulunamadý.");

            updateDto.Adapt(entity);
            entity.EgitimTarihi = DateTime.SpecifyKind(entity.EgitimTarihi, DateTimeKind.Utc);

            _educationRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }

        public async Task<BaseResult<object>> CreateWithParticipantsAsync(CreateEducationWithParticipantsDto dto)
        {
            var educationDto = new CreateEducationDto
            {
                Ad = dto.Ad,
                Egitmen = dto.Egitmen,
                EgitimAciklamasi = dto.EgitimAciklamasi,
                EgitimTarihi = dto.EgitimTarihi,
                SuresiSaat = dto.SuresiSaat
            };

            var validationResult = await _createValidator.ValidateAsync(educationDto);
            if (!validationResult.IsValid)
                return BaseResult<object>.Fail(validationResult.Errors);

            var entity = educationDto.Adapt<Egitim>();
            entity.EgitimTarihi = DateTime.SpecifyKind(entity.EgitimTarihi, DateTimeKind.Utc);

            // ÝÞ KURALI: Geçmiþ tarihli eðitim -> otomatik "Tamamlandý"; bugün/gelecek -> "Planlandý"
            bool gecmisTarihli = entity.EgitimTarihi.Date < DateTime.UtcNow.Date;
            entity.Durumu = gecmisTarihli ? TrainingStatus.Tamamlandi : TrainingStatus.Planlandi;

            await _educationRepository.CreateAsync(entity);

            bool educationSaved = await _unitOfWork.SaveChangesAsync();
            if (!educationSaved)
                return BaseResult<object>.Fail("Eðitim oluþturulamadý.");

            if (dto.SelectedUserIds is { Count: > 0 })
            {
                foreach (var userId in dto.SelectedUserIds.Distinct())
                {
                    await _userEducationRepository.CreateAsync(new AppUserEgitim
                    {
                        AppUserId = userId,
                        EgitimId = entity.Id,
                        BasvuruTarihi = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc),
                        // Eðitim geçmiþte yapýlmýþsa katýlýmcý kaydý da direkt "Tamamlandý" olarak düþer,
                        // aksi halde admin atamasý olduðu için "Onaylandý" (eðitim henüz gerçekleþmedi)
                        BasvuruDurumu = gecmisTarihli ? ApplicationStatus.Tamamlandi : ApplicationStatus.Onaylandi
                    });
                }

                bool participantsSaved = await _unitOfWork.SaveChangesAsync();
                if (!participantsSaved)
                    return BaseResult<object>.Fail("Eðitim oluþturuldu fakat katýlýmcýlar eklenemedi.");
            }

            return BaseResult<object>.Success(new { entity.Id, Message = "Eðitim ve katýlýmcýlar baþarýyla oluþturuldu." });
        }
    }
}