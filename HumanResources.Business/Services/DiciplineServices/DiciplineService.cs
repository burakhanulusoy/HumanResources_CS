using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DiciplineDtos;
using HumanResources.Business.Services.FileServices;
using HumanResources.DataAccess.Repositories.DiciplineRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Services.DiciplineServices
{
    public class DiciplineService : IDiciplineService
    {
        private readonly IDiciplineRepository _diciplineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateDiciplineDto> _createValidator;
        private readonly IValidator<UpdateDiciplineDto> _updateValidator;
        private readonly IFileService _fileService;

        public DiciplineService(IDiciplineRepository diciplineRepository,
                                IUnitOfWork unitOfWork,
                                IValidator<CreateDiciplineDto> createValidator,
                                IValidator<UpdateDiciplineDto> updateValidator,
                                IFileService fileService)
        {
            _diciplineRepository = diciplineRepository;
            _unitOfWork = unitOfWork;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _fileService = fileService;
        }

        public async Task<BaseResult<object>> CreateAsync(CreateDiciplineDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = createDto.Adapt<DisiplinKaydi>();

            if (createDto.Dosya != null)
            {
                string customName = $"Kullanici{createDto.AppUserId}_Olay_{DateTime.Now:yyyyMMdd}";
                entity.DosyaYolu = await _fileService.UploadFileAsync(createDto.Dosya, "diciplines", customName);
            }

            entity.OlayTarihi = DateTime.SpecifyKind(entity.OlayTarihi, DateTimeKind.Utc);

            await _diciplineRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Kayýt oluþturulamadý.");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDiciplineDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _diciplineRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek kayýt bulunamadý.");

            updateDto.Adapt(entity);

            if (updateDto.Dosya != null)
            {
                if (!string.IsNullOrEmpty(entity.DosyaYolu))
                    _fileService.DeleteFile(entity.DosyaYolu); // Eski dosyayý sil

                entity.DosyaYolu = await _fileService.UploadFileAsync(
                    updateDto.Dosya, "diciplines", $"user-{updateDto.AppUserId}");
            }

            entity.OlayTarihi = DateTime.SpecifyKind(entity.OlayTarihi, DateTimeKind.Utc);

            _diciplineRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Güncelleme baþarýsýz.");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _diciplineRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Kayýt bulunamadý.");

            // Kayýt siliniyorsa dosyasý da sunucuda yetim kalmasýn
            if (!string.IsNullOrEmpty(entity.DosyaYolu))
                _fileService.DeleteFile(entity.DosyaYolu);

            _diciplineRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Silme iþlemi baþarýsýz.");
        }

        public async Task<BaseResult<List<ResultDiciplineDto>>> GetAllAsync()
        {
            var entities = await _diciplineRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultDiciplineDto>>();
            return BaseResult<List<ResultDiciplineDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultDiciplineDto>> GetByIdAsync(int id)
        {
            var entity = await _diciplineRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultDiciplineDto>.Fail("Kayýt bulunamadý.");

            return BaseResult<ResultDiciplineDto>.Success(entity.Adapt<ResultDiciplineDto>());
        }

        public async Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId)
        {
            var entities = await _diciplineRepository.GetByUserIdAsync(userId);

            if (entities == null || !entities.Any())
                return BaseResult<List<DiciplineDto>>.Fail("Bu personele ait disiplin veya ödül kaydý bulunamadý.");

            return BaseResult<List<DiciplineDto>>.Success(entities.Adapt<List<DiciplineDto>>());
        }
    }
}
