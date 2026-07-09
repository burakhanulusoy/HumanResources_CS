// UpdateItemValidator.cs
using FluentValidation;
using HumanResources.Business.DTOs.ItemDtos;
using System;
namespace HumanResources.Business.Validators.ItemValidators
{
    public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
    {
        public UpdateItemValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.AppUserId).GreaterThan(0);
            RuleFor(x => x.DemirbasId).GreaterThan(0).WithMessage("Bir demirbaþ seçilmelidir.");
            RuleFor(x => x.TeslimTarihi)
                .NotEmpty().WithMessage("Teslim tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Teslim tarihi bugünden ileri olamaz.");
            RuleFor(x => x.Durumu).IsInEnum().WithMessage("Geçersiz zimmet durumu.");

            RuleFor(x => x.IadeTarihi)
                .NotNull().WithMessage("Süresiz seçili deðilse iade tarihi zorunludur.")
                .GreaterThanOrEqualTo(x => x.TeslimTarihi).WithMessage("Ýade tarihi, teslim tarihinden önce olamaz.")
                .When(x => !x.SuresizMi);

            RuleFor(x => x.Aciklama).MaximumLength(500);
        }
    }
}