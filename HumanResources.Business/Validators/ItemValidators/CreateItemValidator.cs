// CreateItemValidator.cs
using FluentValidation;
using HumanResources.Business.DTOs.ItemDtos;
using System;
namespace HumanResources.Business.Validators.ItemValidators
{
    public class CreateItemValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemValidator()
        {
            RuleFor(x => x.AppUserId).GreaterThan(0).WithMessage("Geçersiz personel kimliði.");
            RuleFor(x => x.DemirbasId).GreaterThan(0).WithMessage("Bir demirbaþ seçilmelidir.");
            RuleFor(x => x.TeslimTarihi)
                .NotEmpty().WithMessage("Teslim tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Teslim tarihi bugünden ileri olamaz.");

            // Süresiz DEÐÝLSE iade tarihi zorunlu ve teslimden sonra
            RuleFor(x => x.IadeTarihi)
                .NotNull().WithMessage("Süresiz seçili deðilse iade tarihi zorunludur.")
                .GreaterThanOrEqualTo(x => x.TeslimTarihi).WithMessage("Ýade tarihi, teslim tarihinden önce olamaz.")
                .When(x => !x.SuresizMi);

            RuleFor(x => x.Aciklama).MaximumLength(500);
        }
    }
}