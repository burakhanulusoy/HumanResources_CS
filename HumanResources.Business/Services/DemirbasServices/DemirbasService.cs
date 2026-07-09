using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.DemirbasDtos;
using HumanResources.DataAccess.Repositories.DemirbasRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.DemirbasServices
{
    public class DemirbasService(IDemirbasRepository _demirbasRepository,
                                 IUnitOfWork _unitOfWork,
                                 IValidator<CreateDemirbasDto> _createValidator,
                                 IValidator<UpdateDemirbasDto> _updateValidator) : IDemirbasService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateDemirbasDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = createDto.Adapt<Demirbas>();
            entity.Durumu = DemirbasDurumu.Musait;   // yeni demirbaþ depoya girer

            await _demirbasRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();
            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateDemirbasDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _demirbasRepository.GetByIdAsync(updateDto.Id);
            if (entity is null) return BaseResult<object>.Fail("Güncellenecek demirbaþ bulunamadý.");

            updateDto.Adapt(entity);
            _demirbasRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();
            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _demirbasRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Demirbaþ bulunamadý.");

            if (entity.Durumu == DemirbasDurumu.Zimmetli)
                return BaseResult<object>.Fail("Zimmetli bir demirbaþ silinemez. Önce iade alýnmalý.");

            // Geçmiþte bu demirbaþa ait zimmet kaydý varsa silme (FK ve tarihçe korunur)
            bool zimmetVar = await _demirbasRepository.HasAnyZimmetAsync(id);
            if (zimmetVar)
                return BaseResult<object>.Fail("Bu demirbaþa ait zimmet kayýtlarý bulunduðu için silinemez.");

            _demirbasRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();
            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }



        public async Task<BaseResult<List<ResultDemirbasDto>>> GetAllAsync()
        {
            var entities = await _demirbasRepository.GetAllAsync();
            return BaseResult<List<ResultDemirbasDto>>.Success(entities.Adapt<List<ResultDemirbasDto>>());
        }

        public async Task<BaseResult<ResultDemirbasDto>> GetByIdAsync(int id)
        {
            var entity = await _demirbasRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultDemirbasDto>.Fail("Demirbaþ bulunamadý.");
            return BaseResult<ResultDemirbasDto>.Success(entity.Adapt<ResultDemirbasDto>());
        }

        public async Task<BaseResult<List<DemirbasDto>>> GetAllWithTypeAsync()
        {
            var entities = await _demirbasRepository.GetAllWithTypeAsync();
            return BaseResult<List<DemirbasDto>>.Success(entities.Adapt<List<DemirbasDto>>());
        }

        public async Task<BaseResult<List<ResultDemirbasDto>>> GetAvailableAsync()
        {
            var entities = await _demirbasRepository.GetAvailableAsync();
            return BaseResult<List<ResultDemirbasDto>>.Success(entities.Adapt<List<ResultDemirbasDto>>());
        }

        public async Task<BaseResult<DemirbasDto>> GetWithTypeByIdAsync(int id)
        {
            var entity = await _demirbasRepository.GetByIdWithTypeAsync(id);
            if (entity is null) return BaseResult<DemirbasDto>.Fail("Demirbaþ bulunamadý.");
            return BaseResult<DemirbasDto>.Success(entity.Adapt<DemirbasDto>());
        }
    }
}