using FluentValidation;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.Validators.UserValidators
{
    public class LoginUserValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanýcý adý boþ býrakýlamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Þifre boþ býrakýlamaz.");
        }
    }
}