using FluentValidation;
using HumanResources.Business.DTOs.UserEducationDtos;

namespace HumanResources.Business.Validators.UserEducationValidators
{
    public class CreateUserEducationValidator : AbstractValidator<CreateUserEducationDto>
    {
        public CreateUserEducationValidator()
        {
            RuleFor(x => x.AppUserId)
                .NotEmpty().WithMessage("Personel seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz personel kimliði.");

            RuleFor(x => x.EgitimId)
                .NotEmpty().WithMessage("Eðitim seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçersiz eðitim kimliði.");
        }
    }
}