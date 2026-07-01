using FluentValidation;
using HumanResources.Business.Base;
using HumanResources.Business.DTOs.PuantajDtos;
using HumanResources.DataAccess.Repositories.PuantajRepositories;
using HumanResources.DataAccess.Repositories.ShiftRepositories;
using HumanResources.DataAccess.UOW;
using HumanResources.Entity.Entities;
using Mapster;

namespace HumanResources.Business.Services.PuantajServices
{
    public class PuantajService(
        IPuantajRepository _puantajRepository,
        IShiftRepository  _vardiyaRepository, // Süre hesaplamasý için vardiya aldýkss
        IUnitOfWork _unitOfWork,
        IValidator<UpdatePuantajDto> _updateValidator,
        IValidator<CreatePuantajDto> _createValidator) : IPuantajService
    {
        public async Task<BaseResult<object>> CreateAsync(CreatePuantajDto createDto)
        {
            var validationResult = await _createValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
                return BaseResult<object>.Fail(validationResult.Errors);

            // AYNI GÜNE ÇÝFT KAYIT KONTROLÜ
            var existingRecord = await _puantajRepository.GetPuantajByUserIdAndDateAsync(createDto.AppUserId, createDto.Tarih);
            if (existingRecord != null)
                return BaseResult<object>.Fail("Bu personel için seçilen tarihte zaten bir puantaj kaydý bulunmaktadýr.");

            var entity = createDto.Adapt<Puantaj>();
            entity.Tarih = DateTime.SpecifyKind(entity.Tarih, DateTimeKind.Utc);

            // SÜRE HESAPLAMA MOTORUNU ÇALIÞTIR
            var vardiya = await _vardiyaRepository.GetByIdAsync(entity.VardiyaId);
            if (vardiya == null) return BaseResult<object>.Fail("Seçilen vardiya bulunamadý.");

            CalculatePuantajTimes(entity, vardiya);

            await _puantajRepository.CreateAsync(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Kayýt oluþturulamadý.");
        }

        public async Task<BaseResult<object>> UpdateAsync(UpdatePuantajDto updateDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateDto);
            if (!validationResult.IsValid) return BaseResult<object>.Fail(validationResult.Errors);

            var entity = await _puantajRepository.GetByIdAsync(updateDto.Id);
            if (entity == null) return BaseResult<object>.Fail("Güncellenecek puantaj bulunamadý.");

            updateDto.Adapt(entity);
            entity.Tarih = DateTime.SpecifyKind(entity.Tarih, DateTimeKind.Utc);

            // SÜRE HESAPLAMA MOTORUNU ÇALIÞTIR
            var vardiya = await _vardiyaRepository.GetByIdAsync(entity.VardiyaId);
            CalculatePuantajTimes(entity, vardiya);

            _puantajRepository.Update(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success(entity) : BaseResult<object>.Fail("Güncelleme baþarýsýz.");
        }

        public async Task<BaseResult<object>> DeleteAsync(int id)
        {
            var entity = await _puantajRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<object>.Fail("Puantaj bulunamadý.");

            _puantajRepository.Delete(entity);
            bool result = await _unitOfWork.SaveChangesAsync();

            return result ? BaseResult<object>.Success() : BaseResult<object>.Fail("Silme baþarýsýz.");
        }

        // --- DÝÐER STANDART GET METOTLARI ---

        public async Task<BaseResult<List<ResultPuantajDto>>> GetAllAsync()
        {
            var entities = await _puantajRepository.GetAllAsync();
            return BaseResult<List<ResultPuantajDto>>.Success(entities.Adapt<List<ResultPuantajDto>>());
        }

        public async Task<BaseResult<ResultPuantajDto>> GetByIdAsync(int id)
        {
            var entity = await _puantajRepository.GetByIdAsync(id);
            if (entity is null) return BaseResult<ResultPuantajDto>.Fail("Puantaj bulunamadý.");
            return BaseResult<ResultPuantajDto>.Success(entity.Adapt<ResultPuantajDto>());
        }

        // --- ÖZEL REPOSITORY METOTLARININ ÇAÐRIMLARI ---

        public async Task<BaseResult<List<PuantajDto>>> GetAllPuantajWithUserAndShiftAsync()
        {
            var entities = await _puantajRepository.GetAllPuantajWithUserAndShiftAsync();
            return BaseResult<List<PuantajDto>>.Success(entities.Adapt<List<PuantajDto>>());
        }

        public async Task<BaseResult<PuantajDto>> GetPuantajByUserIdAndDateAsync(int userId, DateTime date)
        {
            var entity = await _puantajRepository.GetPuantajByUserIdAndDateAsync(userId, date);
            if (entity is null) return BaseResult<PuantajDto>.Fail("Kayýt bulunamadý.");
            return BaseResult<PuantajDto>.Success(entity.Adapt<PuantajDto>());
        }

        public async Task<BaseResult<List<PuantajDto>>> GetPuantajsByUserIdAsync(int userId)
        {
            var entities = await _puantajRepository.GetPuantajsByUserIdAsync(userId);
            return BaseResult<List<PuantajDto>>.Success(entities.Adapt<List<PuantajDto>>());
        }

        public async Task<BaseResult<List<PuantajDto>>> GetPuantajsByUserIdAndDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            var entities = await _puantajRepository.GetPuantajsByUserIdAndDateRangeAsync(userId, startDate, endDate);
            return BaseResult<List<PuantajDto>>.Success(entities.Adapt<List<PuantajDto>>());
        }

        public async Task<BaseResult<List<PuantajDto>>> GetAbsentPuantajsByDateAsync(DateTime date)
        {
            var entities = await _puantajRepository.GetAbsentPuantajsByDateAsync(date);
            return BaseResult<List<PuantajDto>>.Success(entities.Adapt<List<PuantajDto>>());
        }


        // =========================================================================
        // ÝÞTE O SÝHÝRLÝ HESAPLAMA MOTORU (BUSINESS LOGIC)
        // =========================================================================
        private void CalculatePuantajTimes(Puantaj entity, Vardiya vardiya)
        {
            // 1. Durum: Personel devamsýzsa veya saatler boþsa her þey 0'dýr.
            if (entity.Devamsiz || !entity.GirisZamani.HasValue || !entity.CikisZamani.HasValue)
            {
                entity.ToplamCalismaSuresiDk = 0;
                entity.FazlaMesaiDk = 0;
                entity.GecKalmaDk = 0;
                entity.ErkenCikisDk = 0;
                return;
            }

            // Hesaplamayý kolaylaþtýrmak için vardiya baþlangýç ve bitiþini BUGÜNÜN tarihine setliyoruz.
            DateTime vardiyaBaslangic = entity.Tarih.Date.Add(vardiya.BaslangicSaati);
            DateTime vardiyaBitis = entity.Tarih.Date.Add(vardiya.BitisSaati);

            // Gece vardiyasý çözümü: Eðer vardiya bitiþi baþlangýçtan küçükse (örn 22:00 -> 06:00), bitiþ ertesi gündür!
            if (vardiya.BitisSaati < vardiya.BaslangicSaati)
            {
                vardiyaBitis = vardiyaBitis.AddDays(1);
            }

            // A) Geç Kalma (Giriþ saati, vardiya baþlangýcýndan sonraysa)
            if (entity.GirisZamani.Value > vardiyaBaslangic)
                entity.GecKalmaDk = (int)(entity.GirisZamani.Value - vardiyaBaslangic).TotalMinutes;
            else
                entity.GecKalmaDk = 0;

            // B) Erken Çýkýþ (Çýkýþ saati, vardiya bitiþinden önceyse)
            if (entity.CikisZamani.Value < vardiyaBitis)
                entity.ErkenCikisDk = (int)(vardiyaBitis - entity.CikisZamani.Value).TotalMinutes;
            else
                entity.ErkenCikisDk = 0;

            // C) Fazla Mesai (Çýkýþ saati, vardiya bitiþinden sonraysa)
            if (entity.CikisZamani.Value > vardiyaBitis)
                entity.FazlaMesaiDk = (int)(entity.CikisZamani.Value - vardiyaBitis).TotalMinutes;
            else
                entity.FazlaMesaiDk = 0;

            // D) Toplam Çalýþma Süresi (Çýkýþ - Giriþ) EKSÝ (-) Mola Süresi
            var brutCalismaDk = (entity.CikisZamani.Value - entity.GirisZamani.Value).TotalMinutes;
            entity.ToplamCalismaSuresiDk = Math.Max(0, (int)brutCalismaDk - vardiya.AraDinlenmeSuresiDk);
        }
    }
}