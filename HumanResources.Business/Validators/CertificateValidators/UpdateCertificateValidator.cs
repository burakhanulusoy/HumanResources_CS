using FluentValidation;
using HumanResources.Business.DTOs.CertificateDtos;
using HumanResources.Entity.Enums; // CertificateStatus için gerekli
using System;

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
     .GreaterThan(x => x.AlinmaTarihi).WithMessage("Geçerlilik tarihi, alýnma tarihinden sonra olmalýdýr.")
     .When(x => x.Durumu != CertificateStatus.Sinirsiz);   // YENÝ

            RuleFor(x => x.YenilemeTarihi)
                .NotEmpty().WithMessage("Yenileme tarihi zorunludur.")
                .GreaterThan(x => x.AlinmaTarihi).WithMessage("Yenileme tarihi, alýnma tarihinden sonra olmalýdýr.")
                .LessThan(x => x.GecerlilikTarihi).WithMessage("Yenileme tarihi, geçerlilik tarihinden önce olmalýdýr.")
                .When(x => x.Durumu != CertificateStatus.Sinirsiz);   // YENÝ
            RuleFor(x => x.Durumu)
                .IsInEnum().WithMessage("Geçersiz sertifika durumu.");

            // --- ÝSTEM AÇIÐINI KAPATAN KONTROLLER ---

            When(x => x.Durumu == CertificateStatus.Gecerli, () =>
            {
                RuleFor(x => x.GecerlilikTarihi)
                    .GreaterThanOrEqualTo(DateTime.Today)
                    .WithMessage("Durumu 'Geçerli' seçilen bir sertifikanýn geçerlilik tarihi geçmiþ olamaz.");
            });

            When(x => x.Durumu == CertificateStatus.SuresiDolu, () =>
            {
                RuleFor(x => x.GecerlilikTarihi)
                    .LessThan(DateTime.Today)
                    .WithMessage("Geçerlilik tarihi henüz dolmamýþ bir sertifika 'Süresi Dolu' olarak iþaretlenemez.");
            });
        }
    }
}