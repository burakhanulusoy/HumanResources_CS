using FluentValidation;
using HumanResources.Business.DTOs.CertificateTypeDtos;

namespace HumanResources.Business.Validators.CertificateTypeValidators
{
    public class UpdateCertificateTypeValidator : AbstractValidator<UpdateCertificateTypeDto>
    {
        public UpdateCertificateTypeValidator()
        {

            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Sertifika türü adý zorunludur.")
                .MaximumLength(150).WithMessage("Sertifika türü adý en fazla 150 karakter olabilir.");

            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açýklama en fazla 500 karakter olabilir.");
        }
    }
}