using FluentValidation;
using HumanResources.Business.DTOs.RoleDtos;

namespace HumanResources.Business.Validators.RoleValidators
{
    public class UpdateRoleValidator:AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Rol adý boþ býrakýlamaz.")
              .NotNull().WithMessage("Rol adý zorunludur.")
              .MinimumLength(3).WithMessage("Rol adý en az 3 karakter olmalýdýr.")
              .MaximumLength(50).WithMessage("Rol adý en fazla 50 karakter olabilir.");

        }
    }
}
