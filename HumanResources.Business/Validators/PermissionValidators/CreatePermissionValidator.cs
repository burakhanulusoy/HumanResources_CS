using FluentValidation;
using HumanResources.Business.DTOs.PermissionDtos;
using System;

namespace HumanResources.Business.Validators.PermissionValidators
{
    public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
    {
        public CreatePermissionValidator()
        {
            RuleFor(x => x.PersonelId)
                .NotEmpty().WithMessage("Personel seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz personel bilgisi.");

            RuleFor(x => x.IzinTuruId)
                .NotEmpty().WithMessage("Ýzin türü seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz izin türü bilgisi.");

            RuleFor(x => x.BaslangicTarihi)
                .NotEmpty().WithMessage("Baþlangýç tarihi zorunludur.")
                .Must(tarih => tarih.Date >= DateTime.Today)
                .WithMessage("Baþlangýç tarihi bugünden önce olamaz.");

            RuleFor(x => x.BitisTarihi)
                .NotEmpty().WithMessage("Bitiþ tarihi zorunludur.")
                .GreaterThanOrEqualTo(x => x.BaslangicTarihi)
                .WithMessage("Bitiþ tarihi, baþlangýç tarihinden önce olamaz.");

            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Ýzin nedeni (açýklama) boþ geçilemez.")
                .MinimumLength(5).WithMessage("Ýzin nedeni en az 5 karakter olmalýdýr.")
                .MaximumLength(500).WithMessage("Ýzin nedeni çok uzun, lütfen kýsaltýn.");
        }
    }
}