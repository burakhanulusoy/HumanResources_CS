using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.ShiftDtos;
using HumanResources.DataAccess.Repositories.ShiftRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanResources.Business.Services.ShiftServices
{
    public class ShiftService(
        IShiftRepository _shiftRepository,
        IUnitOfWork _unitOfWork,
        IValidator<UpdateShiftDto> _updateValidator,
        IValidator<CreateShiftDto> _createValidator,
        UserManager<AppUser> _userManager) : IShiftService
    {
        public async Task<BaseResult<object>> CreateAsync(CreateShiftDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);
            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var shift = createDto.Adapt<Vardiya>();

            shift.CalismaSuresi = MesaiHesapla(createDto.BaslangicSaati, createDto.BitisSaati, createDto.AraDinlenmeSuresiDk);

            if (createDto.PersonelIds != null && createDto.PersonelIds.Any())
            {
                var personeller = await _userManager.Users
                    .Where(u => createDto.PersonelIds.Contains(u.Id))
                    .ToListAsync();

                shift.Personeller = personeller;
            }

            // Yönetici, seçilen personel listesinde deðilse güvenlik amaçlý null'a çekilir
            if (shift.YoneticiId.HasValue &&
                (createDto.PersonelIds == null || !createDto.PersonelIds.Contains(shift.YoneticiId.Value)))
            {
                shift.YoneticiId = null;
            }

            await _shiftRepository.CreateAsync(shift);
            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(new { Message = "Vardiya baþarýyla oluþturuldu." })
                          : BaseResult<object>.Fail("Vardiya oluþturulurken bir hata meydana geldi.");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdateShiftDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid)
            {
                return BaseResult<object>.Fail(validationResult.Errors);
            }

            var shift = await _shiftRepository.GetQueryable()
                .Include(x => x.Personeller)
                .FirstOrDefaultAsync(x => x.Id == updateDto.Id && !x.SilindiMi);

            if (shift is null)
            {
                return BaseResult<object>.Fail("Güncellenecek vardiya bulunamadý.");
            }

            updateDto.Adapt(shift);

            shift.CalismaSuresi = MesaiHesapla(updateDto.BaslangicSaati, updateDto.BitisSaati, updateDto.AraDinlenmeSuresiDk);

            if (updateDto.PersonelIds != null)
            {
                var yeniPersoneller = await _userManager.Users
                    .Where(u => updateDto.PersonelIds.Contains(u.Id))
                    .ToListAsync();

                shift.Personeller = yeniPersoneller;
            }

            // Yönetici, güncel personel listesinde deðilse güvenlik amaçlý null'a çekilir
            if (shift.YoneticiId.HasValue &&
                (updateDto.PersonelIds == null || !updateDto.PersonelIds.Contains(shift.YoneticiId.Value)))
            {
                shift.YoneticiId = null;
            }

            _shiftRepository.Update(shift);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(new { Message = "Vardiya baþarýyla güncellendi." })
                          : BaseResult<object>.Fail("Vardiya güncellenirken bir hata meydana geldi.");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var shift = await _shiftRepository.GetByIdAsync(id);

            if (shift is null || shift.SilindiMi)
            {
                return BaseResult<object>.Fail("Silinecek vardiya bulunamadý.");
            }

            _shiftRepository.Delete(shift);

            var result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(new { Message = "Vardiya baþarýyla silindi." })
                          : BaseResult<object>.Fail("Vardiya silinirken bir hata meydana geldi.");
        }

        public async Task<BaseResult<List<ResultShiftDto>>> GetAllAsync()
        {
            var shifts = await _shiftRepository.GetQueryable()
                .Where(s => !s.SilindiMi)
                .Include(s => s.Personeller)
                    .ThenInclude(p => p.Departman)
                .Include(s => s.Personeller)
                    .ThenInclude(p => p.Birim)
                .Include(s => s.Yonetici)
                .AsNoTracking()
                .ToListAsync();

            var mappedItem = shifts.Adapt<List<ResultShiftDto>>();

            return BaseResult<List<ResultShiftDto>>.Success(mappedItem);
        }

        public async Task<BaseResult<ResultShiftDto>> GetByIdAsync(int id)
        {
            var shift = await _shiftRepository.GetQueryable()
                .Include(s => s.Personeller)
                    .ThenInclude(p => p.Departman)
                .Include(s => s.Personeller)
                    .ThenInclude(p => p.Birim)
                .Include(s => s.Yonetici)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && !s.SilindiMi);

            if (shift is null)
            {
                return BaseResult<ResultShiftDto>.Fail("Vardiya bulunamadý.");
            }

            var mappedItem = shift.Adapt<ResultShiftDto>();

            return BaseResult<ResultShiftDto>.Success(mappedItem);
        }

        private TimeSpan MesaiHesapla(TimeSpan baslangic, TimeSpan bitis, int molaDk)
        {
            TimeSpan fark;

            if (bitis < baslangic)
            {
                fark = bitis.Add(TimeSpan.FromDays(1)) - baslangic;
            }
            else
            {
                fark = bitis - baslangic;
            }

            return fark.Subtract(TimeSpan.FromMinutes(molaDk));
        }
    }
}