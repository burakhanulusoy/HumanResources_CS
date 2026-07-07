using FluentValidation;
using HumanResources.Business.DTOs.RoleDtos;

namespace HumanResources.Business.Validators.RoleValidators
{
    public class CreateRoleValidator:AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Rol adý boþ býrakýlamaz.")
                .MinimumLength(2).WithMessage("Rol adý en az 2 karakter olmalýdýr.")
                .MaximumLength(50).WithMessage("Rol adý en fazla 50 karakter olabilir.");
        }
    }
}
