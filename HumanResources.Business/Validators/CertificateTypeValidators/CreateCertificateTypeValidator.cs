using FluentValidation;
using HumanResources.Business.DTOs.CertificateTypeDtos;

namespace HumanResources.Business.Validators.CertificateTypeValidators
{
    public class CreateCertificateTypeValidator : AbstractValidator<CreateCertificateTypeDto>
    {
        public CreateCertificateTypeValidator()
        {
            RuleFor(x => x.Ad)
                .NotEmpty().WithMessage("Sertifika türü adý zorunludur.")
                .MaximumLength(150).WithMessage("Sertifika türü adý en fazla 150 karakter olabilir.");

            // Aciklama null olabilir ama eðer girilirse 500 karakteri geçmesin
            RuleFor(x => x.Aciklama)
                .MaximumLength(500).WithMessage("Açýklama en fazla 500 karakter olabilir.");
        }
    }
}