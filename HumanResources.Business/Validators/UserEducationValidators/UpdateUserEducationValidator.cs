using FluentValidation;
using HumanResources.Business.DTOs.UserEducationDtos;
using HumanResources.Entity.Enums;

namespace HumanResources.Business.Validators.UserEducationValidators
{
    public class UpdateUserEducationValidator : AbstractValidator<UpdateUserEducationDto>
    {
        public UpdateUserEducationValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz kayýt kimliði.");

            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Geçersiz personel kimliði.");

            RuleFor(x => x.EgitimId)
                .GreaterThan(0).WithMessage("Geçersiz eðitim kimliði.");

       

            RuleFor(x => x.BasvuruDurumu)
                .IsInEnum().WithMessage("Geçersiz baþvuru durumu seçimi.");

            // 1. KURAL: Eðer durum Reddedildi veya Ýptal Edildi ise açýklama ZORUNLUDUR
            RuleFor(x => x.AdminAciklamasi)
                .NotEmpty().WithMessage("Baþvuru reddedildiðinde veya iptal edildiðinde nedenini (açýklama) yazmak zorunludur.")
                .When(x => x.BasvuruDurumu == ApplicationStatus.Reddedildi || x.BasvuruDurumu == ApplicationStatus.IptalEdildi);

            // 2. KURAL: Durum ne olursa olsun, girilen açýklama 500 karakteri geçemez
            RuleFor(x => x.AdminAciklamasi)
                .MaximumLength(500).WithMessage("Admin açýklamasý en fazla 500 karakter olabilir.");

    
        }
    }
}