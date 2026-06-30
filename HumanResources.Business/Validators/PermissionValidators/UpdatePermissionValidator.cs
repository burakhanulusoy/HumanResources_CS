using FluentValidation;
using HumanResources.Business.DTOs.PermissionDtos;

namespace HumanResources.Business.Validators.PermissionValidators
{
    public class UpdatePermissionValidator:AbstractValidator<UpdatePermissionDto>
    {
        public UpdatePermissionValidator()
        {
            RuleFor(x => x.PersonelId)
                .NotEmpty().WithMessage("Personel seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz personel bilgisi.");

            RuleFor(x => x.IzinTuruId)
                .NotEmpty().WithMessage("Ýzin türü seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz izin türü bilgisi.");

            // 2. Tarih Validasyonu (Kritik Ýþ Kuralý)
            RuleFor(x => x.BaslangicTarihi)
                .NotEmpty().WithMessage("Baþlangýç tarihi zorunludur.");

            RuleFor(x => x.BitisTarihi)
                .NotEmpty().WithMessage("Bitiþ tarihi zorunludur.")
                // Bitiþ tarihi baþlangýçtan küçük olamaz
                .GreaterThanOrEqualTo(x => x.BaslangicTarihi)
                .WithMessage("Bitiþ tarihi, baþlangýç tarihinden önce olamaz.");

            // 3. Açýklama Validasyonu
            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Ýzin nedeni (açýklama) boþ geçilemez.")
                .MinimumLength(5).WithMessage("Ýzin nedeni en az 5 karakter olmalýdýr.")
                .MaximumLength(500).WithMessage("Ýzin nedeni çok uzun, lütfen kýsaltýn.");
        }
    }
}
