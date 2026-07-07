using FluentValidation;
using HumanResources.Business.DTOs.EducationDtos;

namespace HumanResources.Business.Validators.EducationValidators
{
    public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
    {
        public CreateEducationValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Eðitim adý zorunludur.")
                .MaximumLength(150).WithMessage("Eðitim adý en fazla 150 karakter olabilir.");

            RuleFor(x => x.Egitmen)
                .NotEmpty().WithMessage("Eðitmen bilgisi zorunludur.")
                .MaximumLength(100).WithMessage("Eðitmen adý en fazla 100 karakter olabilir.");

            RuleFor(x => x.EgitimAciklamasi)
                .NotEmpty().WithMessage("Eðitim açýklamasý zorunludur.")
                .MinimumLength(10).WithMessage("Eðitim açýklamasý en az 10 karakter olmalýdýr.")
                .MaximumLength(1000).WithMessage("Eðitim açýklamasý en fazla 1000 karakter olabilir.");

            RuleFor(x => x.EgitimTarihi)
                .NotEmpty().WithMessage("Eðitim tarihi zorunludur.")
                .Must(tarih => tarih.Date >= DateTime.Today)
                .WithMessage("Eðitim tarihi bugünden önce olamaz.");

            RuleFor(x => x.SuresiSaat)
                .NotEmpty().WithMessage("Eðitim süresi zorunludur.")
                .GreaterThan(0).WithMessage("Eðitim süresi 0'dan büyük olmalýdýr.");

           
        }
    }
}