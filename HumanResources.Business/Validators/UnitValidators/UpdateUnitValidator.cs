using FluentValidation;
using HumanResources.Business.DTOs.UnitDtos;

namespace HumanResources.Business.Validators.UnitValidators
{
    public class UpdateUnitValidator:AbstractValidator<UpdateUnitDto>
    {
        public UpdateUnitValidator()
        {

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Birim adý boþ býrakýlamaz.")
                .NotNull().WithMessage("Birim adý zorunludur.")
                .MinimumLength(2).WithMessage("Birim adý en az 2 karakter olmalýdýr.")
                .MaximumLength(100).WithMessage("Birim adý en fazla 100 karakter olabilir.");

            RuleFor(x => x.DepartmanId)
                .NotEmpty().WithMessage("Departman seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir departman seçmelisiniz.");
        }
    }
}
