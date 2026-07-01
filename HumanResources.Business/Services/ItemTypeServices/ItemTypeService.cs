using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemTypeDtos;
using HumanResources.DataAccess.Repositories.ItemTypeRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Services.ItemTypeServices
{
    public class ItemTypeService(IItemTypeRepository _itemTypeRepository,
                                 IUnitOfWork _unitOfWork,
                                 IValidator<UpdateItemTypeDto> _updateValidator,
                                 IValidator<CreateItemTypeDto> _createValidator) : IItemTypeService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateItemTypeDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = createDto.Adapt<ZimmetTuru>();

            await _itemTypeRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _itemTypeRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Item Type Not Found");

            _itemTypeRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultItemTypeDto>>> GetAllAsync()
        {
            var entities = await _itemTypeRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultItemTypeDto>>();
            return BaseResult<List<ResultItemTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultItemTypeDto>> GetByIdAsync(int id)
        {
            var entity = await _itemTypeRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultItemTypeDto>.Fail("Item Type Not Found");

            var mappedEntity = entity.Adapt<ResultItemTypeDto>();
            return BaseResult<ResultItemTypeDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateItemTypeDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _itemTypeRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek zimmet türü bulunamadý.");

            updateDto.Adapt(entity);

            _itemTypeRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }


        public async Task<BaseResult<List<ItemTypeDto>>> GetAllItemTypesWithItemsAsync()
        {
            var entities = await _itemTypeRepository.GetAllItemTypesWithItemsAsync();
            var mappedEntities = entities.Adapt<List<ItemTypeDto>>();
            return BaseResult<List<ItemTypeDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ItemTypeDto>> GetItemTypeWithItemsByIdAsync(int id)
        {
            var entity = await _itemTypeRepository.GetItemTypeWithItemsByIdAsync(id);
            if (entity is null) return BaseResult<ItemTypeDto>.Fail("Item Type Not Found");

            var mappedEntity = entity.Adapt<ItemTypeDto>();
            return BaseResult<ItemTypeDto>.Success(mappedEntity);
        }
    }
}