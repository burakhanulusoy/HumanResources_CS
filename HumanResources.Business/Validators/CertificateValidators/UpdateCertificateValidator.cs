using FluentValidation;
using HumanResources.Business.DTOs.CertificateDtos;

namespace HumanResources.Business.Validators.CertificateValidators
{
    public class UpdateCertificateValidator : AbstractValidator<UpdateCertificateDto>
    {
        public UpdateCertificateValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçersiz sertifika kimliði.");

            RuleFor(x => x.AppUserId)
                .GreaterThan(0).WithMessage("Geçersiz personel kimliði.");

            RuleFor(x => x.SertifikaTuruId)
                .GreaterThan(0).WithMessage("Geçersiz sertifika türü kimliði.");

            RuleFor(x => x.VerenKurum)
                .NotEmpty().WithMessage("Veren kurum bilgisi zorunludur.")
                .MaximumLength(150).WithMessage("Veren kurum adý en fazla 150 karakter olabilir.");

            RuleFor(x => x.BelgeNo)
                .NotEmpty().WithMessage("Belge numarasý zorunludur.")
                .MaximumLength(50).WithMessage("Belge numarasý en fazla 50 karakter olabilir.");

            RuleFor(x => x.AlinmaTarihi)
                .NotEmpty().WithMessage("Alýnma tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Alýnma tarihi bugünden ileri bir tarih olamaz.");

            RuleFor(x => x.GecerlilikTarihi)
                .NotEmpty().WithMessage("Geçerlilik tarihi zorunludur.")
                .GreaterThan(x => x.AlinmaTarihi).WithMessage("Geçerlilik tarihi, alýnma tarihinden sonra olmalýdýr.");

            RuleFor(x => x.YenilemeTarihi)
                .NotEmpty().WithMessage("Yenileme tarihi zorunludur.")
                .GreaterThan(x => x.AlinmaTarihi).WithMessage("Yenileme tarihi, alýnma tarihinden sonra olmalýdýr.");

            // Sertifika statüsü enum içerisinden (Geçerli, SüresiDolu, ÝptalEdildi vb.) seçilmek zorunda
            RuleFor(x => x.Durumu)
                .IsInEnum().WithMessage("Geçersiz sertifika durumu.");
        }
    }
}