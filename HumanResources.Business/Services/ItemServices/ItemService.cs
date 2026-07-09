using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.DataAccess.Repositories.DemirbasRepositories;
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
                             IValidator<CreateItemDto> _createValidator,
                              IDemirbasRepository _demirbasRepository) : IItemService
    {


        public async Task<BaseResult<object>> CreateAsync(CreateItemDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var demirbas = await _demirbasRepository.GetByIdAsync(createDto.DemirbasId);
            if (demirbas is null) return BaseResult<object>.Fail("Seçilen demirbaþ bulunamadý.");
            if (demirbas.Durumu == DemirbasDurumu.Zimmetli)
                return BaseResult<object>.Fail("Bu demirbaþ zaten baþka bir personelde zimmetli.");

            var entity = createDto.Adapt<Zimmet>();
            entity.Durumu = ZimmetDurumu.Aktif;

            entity.TeslimTarihi = DateTime.SpecifyKind(entity.TeslimTarihi, DateTimeKind.Utc);
            if (entity.SuresizMi)
                entity.IadeTarihi = null;
            else if (entity.IadeTarihi.HasValue)
                entity.IadeTarihi = DateTime.SpecifyKind(entity.IadeTarihi.Value, DateTimeKind.Utc);

            await _itemRepository.CreateAsync(entity);

            // Yeni zimmet aktif -> demirbaþ zimmetli olur
            demirbas.Durumu = DemirbasDurumu.Zimmetli;
            _demirbasRepository.Update(demirbas);

            bool result = await _unitOfWork.SaveChangesAsync();
            return result
                ? BaseResult<object>.Success(new { Message = "Zimmet baþarýyla oluþturuldu.", entity.Id })
                : BaseResult<object>.Fail("Created Failed");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateItemDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _itemRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek zimmet bulunamadý.");

            int eskiDemirbasId = entity.DemirbasId;   // deðiþiklik öncesi demirbaþ

            updateDto.Adapt(entity);

            entity.TeslimTarihi = DateTime.SpecifyKind(entity.TeslimTarihi, DateTimeKind.Utc);
            if (entity.SuresizMi)
                entity.IadeTarihi = null;
            else if (entity.IadeTarihi.HasValue)
                entity.IadeTarihi = DateTime.SpecifyKind(entity.IadeTarihi.Value, DateTimeKind.Utc);

            // --- 1) DEMÝRBAÞ DEÐÝÞTÝRÝLDÝ MÝ? ---
            if (eskiDemirbasId != entity.DemirbasId)
            {
                var yeniDemirbas = await _demirbasRepository.GetByIdAsync(entity.DemirbasId);
                if (yeniDemirbas is null)
                    return BaseResult<object>.Fail("Seçilen yeni demirbaþ bulunamadý.");
                if (yeniDemirbas.Durumu == DemirbasDurumu.Zimmetli)
                    return BaseResult<object>.Fail("Seçilen yeni demirbaþ zaten baþka bir personelde zimmetli.");

                // Eski demirbaþý serbest býrak
                var eskiDemirbas = await _demirbasRepository.GetByIdAsync(eskiDemirbasId);
                if (eskiDemirbas is not null)
                {
                    eskiDemirbas.Durumu = DemirbasDurumu.Musait;
                    _demirbasRepository.Update(eskiDemirbas);
                }
            }

            _itemRepository.Update(entity);

            // --- 2) ZÝMMET DURUMU -> DEMÝRBAÞ DURUMU SENKRONU ---
            // Zimmet hangi duruma güncellendiyse, güncel demirbaþý da ona göre ayarla
            var guncelDemirbas = await _demirbasRepository.GetByIdAsync(entity.DemirbasId);
            if (guncelDemirbas is not null)
            {
                guncelDemirbas.Durumu = entity.Durumu switch
                {
                    ZimmetDurumu.Aktif => DemirbasDurumu.Zimmetli,   // hâlâ birinde
                    ZimmetDurumu.IadeEdildi => DemirbasDurumu.Musait,     // geri geldi
                    ZimmetDurumu.Arizali => DemirbasDurumu.Arizali,    // bozuk
                    ZimmetDurumu.Kayip => DemirbasDurumu.HizmetDisi, // kayýp -> envanterden düþer
                    _ => guncelDemirbas.Durumu
                };
                _demirbasRepository.Update(guncelDemirbas);
            }

            bool result = await _unitOfWork.SaveChangesAsync();
            return result
                ? BaseResult<object>.Success(new { Message = "Zimmet baþarýyla güncellendi.", entity.Id })
                : BaseResult<object>.Fail("Updated Failed");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _itemRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Item Not Found");

            // Zimmet siliniyorsa demirbaþ boþa çýkar -> Müsait yap
            var demirbas = await _demirbasRepository.GetByIdAsync(entity.DemirbasId);
            if (demirbas is not null && demirbas.Durumu == DemirbasDurumu.Zimmetli)
            {
                demirbas.Durumu = DemirbasDurumu.Musait;
                _demirbasRepository.Update(demirbas);
            }

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



        // --- Özel Metotlar ---

        public async Task<BaseResult<List<ItemDto>>> GetAllItemsWithDetailsAsync()
        {
            var entities = await _itemRepository.GetAllItemsWithDetailsAsync();

            TypeAdapterConfig<AppUser, HumanResources.Business.DTOs.UserDtos.UserDto>.NewConfig()
                .Map(dest => dest.AmirAdSoyad, src => src.Amir != null ? src.Amir.Ad + " " + src.Amir.Soyad : null)
                .Map(dest => dest.DepartmanAd, src => src.Departman != null ? src.Departman.Ad : null)
                .Map(dest => dest.BirimAd, src => src.Birim != null ? src.Birim.Ad : null);

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