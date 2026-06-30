using FluentValidation;
using HumanResources.Business.DTOs.PermissionTypeDtos;

namespace HumanResources.Business.Validators.PermissionValidators
{
    public class UpdatePermissionTypeValidator:AbstractValidator<UpdatePermissionTypeDto>
    {
        public UpdatePermissionTypeValidator()
        {
            RuleFor(x => x.Ad)
              .NotEmpty().WithMessage("Ýzin türü adý boþ geçilemez.")
              .MinimumLength(2).WithMessage("Ýzin türü adý en az 2 karakter olmalýdýr.")
              .MaximumLength(50).WithMessage("Ýzin türü adý en fazla 50 karakter olabilir.");
        }
    }
}
