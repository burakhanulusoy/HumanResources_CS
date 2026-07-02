using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DiciplineDtos;
using HumanResources.Business.Services.FileServices;
using HumanResources.DataAccess.Repositories.DiciplineRepositories; // Repository namespace'ini projene göre düzeltirsin
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
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
                {
                    _fileService.DeleteFile(entity.DosyaYolu); // Eski dosyayý sunucudan sil
                }

                string customName = $"Kullanici{updateDto.AppUserId}_Olay_{DateTime.Now:yyyyMMdd}";
                entity.DosyaYolu = await _fileService.UploadFileAsync(updateDto.Dosya, "diciplines", customName);
            }

            entity.OlayTarihi = DateTime.SpecifyKind(entity.OlayTarihi, DateTimeKind.Utc);

            _diciplineRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _diciplineRepository.GetByIdAsync(id);

            if (entity is null) return BaseResult<object>.Fail("Discipline Record Not Found");

        

            _diciplineRepository.Delete(entity); // Pasife alýnmýþ halini güncelliyoruz
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
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

            if (entity is null) return BaseResult<ResultDiciplineDto>.Fail("Discipline Record Not Found");

            var mappedEntity = entity.Adapt<ResultDiciplineDto>();
            return BaseResult<ResultDiciplineDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<List<DiciplineDto>>> GetByUserIdAsync(int userId)
        {
            var entities = await _diciplineRepository.GetByUserIdAsync(userId);

            if (entities == null || !entities.Any())
            {
                return BaseResult<List<DiciplineDto>>.Fail("Bu personele ait herhangi bir disiplin veya ödül kaydý bulunamadý.");
            }

            var mappedEntities = entities.Adapt<List<DiciplineDto>>();
            return BaseResult<List<DiciplineDto>>.Success(mappedEntities);
        }
    }
}