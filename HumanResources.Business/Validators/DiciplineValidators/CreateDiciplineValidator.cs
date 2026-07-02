using FluentValidation;
using HumanResources.Business.DTOs.DiciplineDtos;
using System;

namespace HumanResources.Business.Validators.DiciplineValidators
{
    public class CreateDiciplineValidator : AbstractValidator<CreateDiciplineDto>
    {
        public CreateDiciplineValidator()
        {
            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Geçerli bir personel seçilmelidir.");

            RuleFor(x => x.DisiplinNedeni)
                .NotEmpty().WithMessage("Disiplin nedeni boþ býrakýlamaz.")
                .MaximumLength(150).WithMessage("Disiplin nedeni en fazla 150 karakter olabilir.");

            RuleFor(x => x.Detay)
                .NotEmpty().WithMessage("Detay alaný boþ býrakýlamaz.")
                .MaximumLength(1500).WithMessage("Detay alaný en fazla 1500 karakter olabilir.");

            RuleFor(x => x.OlayTarihi)
                .NotEmpty().WithMessage("Olay tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1)).WithMessage("Olay tarihi gelecekteki bir tarih olamaz.");
        }
    }
}