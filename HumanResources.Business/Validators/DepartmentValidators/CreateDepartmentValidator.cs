using FluentValidation;
using HumanResources.Business.DTOs.DepartmentDtos;

namespace HumanResources.Business.Validators.DepartmentValidators
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Departman adý boþ býrakýlamaz.")
                .NotNull().WithMessage("Departman adý girilmelidir.")
                .MinimumLength(2).WithMessage("Departman adý en az 2 karakter olmalýdýr.")
                .MaximumLength(100).WithMessage("Departman adý en fazla 100 karakter olabilir.");

            RuleFor(x => x.YoneticiId)
                .NotEmpty().WithMessage("Yönetici seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir yönetici seçiniz.");
        }
    }
}