using FluentValidation;
using HumanResources.Business.DTOs.PuantajDtos;
using System;

namespace HumanResources.Business.ValidationRules.PuantajValidators
{
    public class CreatePuantajDtoValidator : AbstractValidator<CreatePuantajDto>
    {
        public CreatePuantajDtoValidator()
        {
            // ID ve Tarih Kontrolleri
            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Personel seçimi zorunludur.");

            RuleFor(x => x.VardiyaId)
                .GreaterThan(0).WithMessage("Vardiya seçimi zorunludur.");

            RuleFor(x => x.Tarih)
                .NotEmpty().WithMessage("Tarih alaný boþ geçilemez.")
                .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1)).WithMessage("Ýleri bir tarihe puantaj girilemez."); // Gelecek tarihe kayýt atýlmasýný önlemek iyi bir pratiktir.

            // Giriþ - Çýkýþ Zamaný Mantýk Kontrolü
            // Eðer personel devamsýz DEÐÝLSE ve Giriþ/Çýkýþ zamaný girilmiþse Çýkýþ, Giriþten büyük olmalýdýr.
            RuleFor(x => x.CikisZamani)
                .GreaterThan(x => x.GirisZamani)
                .When(x => x.GirisZamani.HasValue && x.CikisZamani.HasValue && !x.Devamsiz)
                .WithMessage("Çýkýþ zamaný, giriþ zamanýndan daha önce olamaz.");
        }
    }
}