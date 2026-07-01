using FluentValidation;
using HumanResources.Business.DTOs.ItemDtos;
using System;

namespace HumanResources.Business.Validators.ItemValidators
{
    public class CreateItemValidator : AbstractValidator<CreateItemDto>
    {
        public CreateItemValidator()
        {
            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Geçersiz personel kimliði.");

            RuleFor(x => x.ZimmetTuruId)
                .GreaterThan(0).WithMessage("Geçersiz zimmet türü kimliði.");

            RuleFor(x => x.SeriNumarasi)
                .NotEmpty().WithMessage("Seri numarasý zorunludur.")
                .MaximumLength(100).WithMessage("Seri numarasý en fazla 100 karakter olabilir.");

            RuleFor(x => x.TeslimTarihi)
                .NotEmpty().WithMessage("Teslim tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Teslim tarihi bugünden ileri bir tarih olamaz.");

            RuleFor(x => x.IadeTarihi)
                .NotEmpty().WithMessage("Ýade tarihi zorunludur.")
                .GreaterThanOrEqualTo(x => x.TeslimTarihi).WithMessage("Ýade tarihi, teslim tarihinden önce olamaz.");

            RuleFor(x => x.Aciklama)
                .NotEmpty().WithMessage("Zimmet açýklamasý zorunludur.")
                .MaximumLength(500).WithMessage("Zimmet açýklamasý en fazla 500 karakter olabilir.");
        }
    }
}