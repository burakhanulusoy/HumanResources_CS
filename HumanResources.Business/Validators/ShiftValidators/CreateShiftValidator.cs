using FluentValidation;
using HumanResources.Business.DTOs.ShiftDtos;

namespace HumanResources.Business.Validators.ShiftValidators
{
    public class CreateShiftValidator:AbstractValidator<CreateShiftDto>
    {
        public CreateShiftValidator()
        {
            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Vardiya açýklamasý boþ geçilemez.")
                .MinimumLength(2).WithMessage("Vardiya açýklamasý en az 2 karakter olmalýdýr.")
                .MaximumLength(100).WithMessage("Vardiya açýklamasý en fazla 100 karakter olabilir.");

            RuleFor(x => x.BaslangicSaati)
                .NotNull().WithMessage("Baþlangýç saati girilmesi zorunludur.");

            RuleFor(x => x.BitisSaati)
                .NotNull().WithMessage("Bitiþ saati girilmesi zorunludur.");

            RuleFor(x => x.AraDinlenmeSuresiDk)
                .GreaterThanOrEqualTo(0).WithMessage("Ara dinlenme süresi negatif bir deðer olamaz.")
                .LessThan(480).WithMessage("Ara dinlenme süresi çok uzun (Maksimum 8 saat / 480 dk olabilir).");
        }


    }
}
