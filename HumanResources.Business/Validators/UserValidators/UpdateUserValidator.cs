using FluentValidation;
using HumanResources.Business.DTOs.UserDtos;

namespace HumanResources.Business.Validators.UserValidators
{
    public class UpdateUserValidator:AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.UserName)
              .NotNull().WithMessage("Kullanýcý adý boþ olamaz.")
              .NotEmpty().WithMessage("Kullanýcý adý boþ olamaz.")
              .MinimumLength(3).WithMessage("Kullanýcý adý en az 3 karakter olmalýdýr.")
              .MaximumLength(50).WithMessage("Kullanýcý adý en fazla 50 karakter olabilir.")
              .Matches(@"^[a-zA-Z0-9_\.]+$").WithMessage("Kullanýcý adý yalnýzca harf, rakam, alt çizgi ve nokta içerebilir.")
              .Must(u => !string.IsNullOrWhiteSpace(u)).WithMessage("Kullanýcý adý yalnýzca boþluklardan oluþamaz.");

            RuleFor(x => x.Email)
                .NotNull().WithMessage("E-posta adresi boþ olamaz.")
                .NotEmpty().WithMessage("E-posta adresi boþ olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(150).WithMessage("E-posta adresi en fazla 150 karakter olabilir.");

            RuleFor(x => x.Password)
                .NotNull().WithMessage("Þifre boþ olamaz.")
                .NotEmpty().WithMessage("Þifre boþ olamaz.")
                .MinimumLength(8).WithMessage("Þifre en az 8 karakter olmalýdýr.")
                .MaximumLength(100).WithMessage("Þifre en fazla 100 karakter olabilir.")
                .Matches(@"[A-Z]").WithMessage("Þifre en az bir büyük harf içermelidir.")
                .Matches(@"[a-z]").WithMessage("Þifre en az bir küçük harf içermelidir.")
                .Matches(@"[0-9]").WithMessage("Þifre en az bir rakam içermelidir.")
                .Matches(@"[^a-zA-Z0-9]").WithMessage("Þifre en az bir özel karakter içermelidir (!@#$%^&* vb.)");

            RuleFor(x => x.PhoneNumber)
                .NotNull().WithMessage("Telefon numarasý boþ olamaz.")
                .NotEmpty().WithMessage("Telefon numarasý boþ olamaz.")
                .Matches(@"^(\+90|0)?[5][0-9]{9}$").WithMessage("Geçerli bir Türkiye cep telefonu numarasý giriniz. (05XX XXX XX XX)");


            RuleFor(x => x.SicilNo)
                .NotNull().WithMessage("Sicil numarasý boþ olamaz.")
                .NotEmpty().WithMessage("Sicil numarasý boþ olamaz.")
                .MaximumLength(20).WithMessage("Sicil numarasý en fazla 20 karakter olabilir.")
                .Matches(@"^[a-zA-Z0-9\-]+$").WithMessage("Sicil numarasý yalnýzca harf, rakam ve tire içerebilir.");

            RuleFor(x => x.Ad)
                .NotNull().WithMessage("Ad boþ olamaz.")
                .NotEmpty().WithMessage("Ad boþ olamaz.")
                .MinimumLength(2).WithMessage("Ad en az 2 karakter olmalýdýr.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Soyad)
                .NotNull().WithMessage("Soyad boþ olamaz.")
                .NotEmpty().WithMessage("Soyad boþ olamaz.")
                .MinimumLength(2).WithMessage("Soyad en az 2 karakter olmalýdýr.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.TcKimlikNo)
                .NotNull().WithMessage("TC Kimlik No boþ olamaz.")
                .NotEmpty().WithMessage("TC Kimlik No boþ olamaz.")
                .Length(11).WithMessage("TC Kimlik No 11 haneli olmalýdýr.")
                .Matches(@"^[1-9][0-9]{10}$").WithMessage("TC Kimlik No geçersiz. Ýlk hane 0 olamaz ve yalnýzca rakam içermelidir.")
                .Must(TcKimlikDogrula).WithMessage("Geçerli bir TC Kimlik No giriniz.");

            RuleFor(x => x.DogumTarihi)
                .NotNull().WithMessage("Doðum tarihi boþ olamaz.")
                .GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Geçerli bir doðum tarihi giriniz.");

            RuleFor(x => x.Cinsiyet)
                .NotNull().WithMessage("Cinsiyet boþ olamaz.")
                .NotEmpty().WithMessage("Cinsiyet boþ olamaz.");


            RuleFor(x => x.MedeniDurum)
                .NotNull().WithMessage("Medeni durum boþ olamaz.")
                .NotEmpty().WithMessage("Medeni durum boþ olamaz.");


            RuleFor(x => x.KanGrubu)
                .NotNull().WithMessage("Kan grubu boþ olamaz.")
                .NotEmpty().WithMessage("Kan grubu boþ olamaz.");




            RuleFor(x => x.Adres)
                .NotNull().WithMessage("Adres boþ olamaz.")
                .NotEmpty().WithMessage("Adres boþ olamaz.")
                .MinimumLength(10).WithMessage("Adres en az 10 karakter olmalýdýr.")
                .MaximumLength(300).WithMessage("Adres en fazla 300 karakter olabilir.");

            RuleFor(x => x.AcilDurumKisiAdSoyad)
                .NotNull().WithMessage("Acil durum kiþisinin adý soyadý boþ olamaz.")
                .NotEmpty().WithMessage("Acil durum kiþisinin adý soyadý boþ olamaz.")
                .MinimumLength(5).WithMessage("Acil durum kiþisinin adý soyadý en az 5 karakter olmalýdýr.")
                .MaximumLength(100).WithMessage("Acil durum kiþisinin adý soyadý en fazla 100 karakter olabilir.");

            RuleFor(x => x.AcilDurumTelefonu)
                .NotNull().WithMessage("Acil durum telefonu boþ olamaz.")
                .NotEmpty().WithMessage("Acil durum telefonu boþ olamaz.");



            RuleFor(x => x.DepartmanId)
                .NotNull().WithMessage("Departman seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir departman seçiniz.");

            RuleFor(x => x.BirimId)
                .NotNull().WithMessage("Birim seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir birim seçiniz.");

            RuleFor(x => x.AmirId)
                .NotNull().WithMessage("Amir seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir amir seçiniz.");

            RuleFor(x => x.VardiyaId)
                .NotNull().WithMessage("Vardiya seçimi zorunludur.")
                .GreaterThan(0).WithMessage("Geçerli bir vardiya seçiniz.");

            RuleFor(x => x.CalismaDurumu)
                .NotNull().WithMessage("Çalýþma durumu boþ olamaz.")
                .NotEmpty().WithMessage("Çalýþma durumu boþ olamaz.");


            RuleFor(x => x.PersonelTipi)
                .NotNull().WithMessage("Personel tipi boþ olamaz.")
                .NotEmpty().WithMessage("Personel tipi boþ olamaz.");


            RuleFor(x => x.SgkSicilNo)
                .NotNull().WithMessage("SGK sicil numarasý boþ olamaz.")
                .NotEmpty().WithMessage("SGK sicil numarasý boþ olamaz.");
        }

        private bool TcKimlikDogrula(string? tc)
        {
            if (string.IsNullOrWhiteSpace(tc) || tc.Length != 11) return false;
            if (!tc.All(char.IsDigit)) return false;
            if (tc[0] == '0') return false;

            int[] digits = tc.Select(c => int.Parse(c.ToString())).ToArray();

            // 10. hane kontrolü: (1,3,5,7,9. hanelerin toplamý * 7 - 2,4,6,8. hanelerin toplamý) % 10
            int onuncuHane = ((digits[0] + digits[2] + digits[4] + digits[6] + digits[8]) * 7
                              - (digits[1] + digits[3] + digits[5] + digits[7])) % 10;

            // 11. hane kontrolü: Ýlk 10 hanenin toplamý % 10
            int onbirinciHane = (digits[0] + digits[1] + digits[2] + digits[3] + digits[4]
                                 + digits[5] + digits[6] + digits[7] + digits[8] + digits[9]) % 10;

            return digits[9] == onuncuHane && digits[10] == onbirinciHane;
        }
    }
}

