using FluentValidation;
using HumanResources.Business.DTOs.ItemTypeDtos;

namespace HumanResources.Business.Validators.ItemTypeValidators
{
    public class UpdateItemTypeValidator : AbstractValidator<UpdateItemTypeDto>
    {
        public UpdateItemTypeValidator()
        {
           

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Zimmet türü adý zorunludur.")
                .MaximumLength(100).WithMessage("Zimmet türü adý en fazla 100 karakter olabilir.");
        }
    }
}