using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.DataAccess.Repositories.ItemRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using HumanResources.Entity.Enums;
using Mapster;

namespace HumanResources.Business.Services.ItemServices
{
    public class ItemService(IItemRepository _itemRepository,
                             IUnitOfWork _unitOfWork,
                             IValidator<UpdateItemDto> _updateValidator,
                             IValidator<CreateItemDto> _createValidator) : IItemService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateItemDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = createDto.Adapt<Zimmet>();

            entity.TeslimTarihi = DateTime.SpecifyKind(entity.TeslimTarihi, DateTimeKind.Utc);


            entity.IadeTarihi = DateTime.SpecifyKind(entity.IadeTarihi, DateTimeKind.Utc); 
            
            entity.Durumu = ZimmetDurumu.Aktif;

            await _itemRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _itemRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Item Not Found");

            _itemRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Deleted Failed");
        }

        public async Task<BaseResult<List<ResultItemDto>>> GetAllAsync()
        {
            var entities = await _itemRepository.GetAllAsync();
            var mappedEntities = entities.Adapt<List<ResultItemDto>>();
            return BaseResult<List<ResultItemDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<ResultItemDto>> GetByIdAsync(int id)
        {
            var entity = await _itemRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultItemDto>.Fail("Item Not Found");

            var mappedEntity = entity.Adapt<ResultItemDto>();
            return BaseResult<ResultItemDto>.Success(mappedEntity);
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateItemDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _itemRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek zimmet bulunamadý.");

            updateDto.Adapt(entity);

            // ÝÞ KURALI 3: Tarihleri kontrol et ve UTC'ye çevir
            entity.TeslimTarihi = DateTime.SpecifyKind(entity.TeslimTarihi, DateTimeKind.Utc);
               
            entity.IadeTarihi = DateTime.SpecifyKind(entity.IadeTarihi, DateTimeKind.Utc);

            _itemRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Updated Failed");
        }

        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ItemDto>>> GetAllItemsWithDetailsAsync()
        {
            var entities = await _itemRepository.GetAllItemsWithDetailsAsync();
            var mappedEntities = entities.Adapt<List<ItemDto>>();
            return BaseResult<List<ItemDto>>.Success(mappedEntities);
        }

        public async Task<BaseResult<List<ItemDto>>> GetItemsByUserIdAsync(int userId)
        {
            var entities = await _itemRepository.GetItemsByUserIdAsync(userId);

            if (entities == null || !entities.Any())
            {
                return BaseResult<List<ItemDto>>.Fail("Bu personele ait herhangi bir zimmet kaydý bulunamadý.");
            }

            var mappedEntities = entities.Adapt<List<ItemDto>>();
            return BaseResult<List<ItemDto>>.Success(mappedEntities);
        }

      public async Task<BaseResult<ItemDto>> GetItemWithDetailsByIdAsync(int id)
{
    var entity = await _itemRepository.GetItemWithDetailsByIdAsync(id);
    if (entity is null) return BaseResult<ItemDto>.Fail("Item Not Found");

    // Amir/Departman/Birim adlarýný flatten map et (UserDto bu alanlarý düz string olarak bekliyor)
    TypeAdapterConfig<AppUser, HumanResources.Business.DTOs.UserDtos.UserDto>.NewConfig()
        .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
        .Map(dest => dest.DepartmanAd, src => src.Departman != null ? src.Departman.Ad : null)
        .Map(dest => dest.BirimAd, src => src.Birim != null ? src.Birim.Ad : null);

    var mappedEntity = entity.Adapt<ItemDto>();
    return BaseResult<ItemDto>.Success(mappedEntity);
}
    }
}