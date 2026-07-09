// CreateDemirbasValidator.cs
using FluentValidation;
using HumanResources.Business.DTOs.DemirbasDtos;
namespace HumanResources.Business.Validators.DemirbasValidators
{
    public class CreateDemirbasValidator : AbstractValidator<CreateDemirbasDto>
    {
        public CreateDemirbasValidator()
        {
            RuleFor(x => x.ZimmetTuruId).GreaterThan(0).WithMessage("Zimmet türü seçilmelidir.");
            RuleFor(x => x.Marka).NotEmpty().WithMessage("Marka zorunludur.").MaximumLength(100);
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model zorunludur.").MaximumLength(100);
            RuleFor(x => x.SeriNumarasi).MaximumLength(100);
            RuleFor(x => x.Aciklama).MaximumLength(500);
        }
    }
}