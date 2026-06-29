using FluentValidation;
using HumanResources.Business.DTOs.RoleDtos;

namespace HumanResources.Business.Validators.RoleValidators
{
    public class CreateRoleValidator:AbstractValidator<CreateRoleDto>
    {
        public CreateRoleValidator()
        {
            
        }
    }
}
