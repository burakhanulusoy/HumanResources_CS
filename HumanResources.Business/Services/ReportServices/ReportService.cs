using HumanResources.Business.Base;
using HumanResources.Business.Helpers;
using HumanResources.Business.Services.CertificateServices;
using HumanResources.Business.Services.ItemServices;
using HumanResources.Business.Services.PermissionServices;
using HumanResources.Business.Services.ShiftServices;
using HumanResources.Business.Services.UserEducationServices;
using HumanResources.Business.Services.UserServices;
// NOT: IItemService (Zimmet) projenizde varsa onu da using'e ekleyin.

namespace HumanResources.Business.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IUserEducationService _educationService;
        private readonly ICertificateService _certificateService;
        private readonly IShiftService _shiftService;
        private readonly IItemService _itemService; // Zimmet servisin varsa aç

        public ReportService(
            IUserService userService,
            IPermissionService permissionService,
            IUserEducationService educationService,
            ICertificateService certificateService,
            IShiftService shiftService,
            IItemService itemService)
        {
            _userService = userService;
            _permissionService = permissionService;
            _educationService = educationService;
            _certificateService = certificateService;
            _shiftService = shiftService;
            _itemService = itemService;
        }

        // 1. Personel Listesi
        // 1. Personel Listesi (A'dan Z'ye Tüm Bilgiler)
        public async Task<BaseResult<byte[]>> GetPersonnelListExcelAsync()
        {
            var result = await _userService.GetAllUsersForReportAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            var exportData = result.Data.Select(x => new
            {
                Id = x.Id,
                SicilNo = x.SicilNo,
                Ad = x.Ad,
                Soyad = x.Soyad,
                TcKimlikNo = x.TcKimlikNo,
                KullaniciAdi = x.UserName,
                Email = x.Email,
                Telefon = string.IsNullOrEmpty(x.PhoneNumber) ? "Belirtilmemiþ" : x.PhoneNumber,

                // Nullable (DateTime?) olanlar için HasValue kontrolü
                DogumTarihi = x.DogumTarihi.HasValue ? x.DogumTarihi.Value.ToString("dd.MM.yyyy") : "",
                Cinsiyet = x.Cinsiyet,
                MedeniDurum = x.MedeniDurum,
                KanGrubu = x.KanGrubu,

                Adres = x.Adres,
                AcilDurumKisi = x.AcilDurumKisiAdSoyad,
                AcilDurumTelefon = x.AcilDurumTelefonu,

                // Ýþ Bilgileri
                IseGirisTarihi = x.IseGirisTarihi.HasValue ? x.IseGirisTarihi.Value.ToString("dd.MM.yyyy") : "",
                IstenAyrilisTarihi = x.IstenAyrilisTarihi.HasValue ? x.IstenAyrilisTarihi.Value.ToString("dd.MM.yyyy") : "Devam Ediyor",
                CalismaDurumu = x.CalismaDurumu,
                PersonelTipi = x.PersonelTipi,
                SgkSicilNo = x.SgkSicilNo,

                // Ýliþkiler
                Departman = x.Departman != null ? x.Departman.Ad : "Atanmadý",
                Birim = x.Birim != null ? x.Birim.Ad : "Atanmadý",
                Amir = !string.IsNullOrEmpty(x.AmirAdSoyad) ? x.AmirAdSoyad : "Yok",
                Roller = x.Roller != null && x.Roller.Any() ? string.Join(", ", x.Roller) : "Personel",

                // Normal (DateTime) olanlar asla null olmaz, direkt ToString() yapýyoruz
                OlusturulmaTarihi = x.OlusturulmaTarihi.ToString("dd.MM.yyyy HH:mm"),
                GuncellenmeTarihi = x.GuncellenmeTarihi.ToString("dd.MM.yyyy HH:mm"),

                Durum = x.SilindiMi ? "Pasif" : "Aktif"
            }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Personel Listesi");
            return BaseResult<byte[]>.Success(fileBytes);
        }

        // 2. Departman Bazlý Personel (Departmana göre gruplu ve tüm bilgiler)
        public async Task<BaseResult<byte[]>> GetDepartmentBasedPersonnelExcelAsync()
        {
            var result = await _userService.GetAllUsersForReportAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            var exportData = result.Data
                .OrderBy(x => x.Departman != null ? x.Departman.Ad : "Z_Atanmayanlar") // Departmana göre sýrala
                .Select(x => new
                {
                    Departman = x.Departman != null ? x.Departman.Ad : "Atanmadý",
                    Birim = x.Birim != null ? x.Birim.Ad : "Atanmadý",
                    SicilNo = x.SicilNo,
                    Ad = x.Ad,
                    Soyad = x.Soyad,
                    TcKimlikNo = x.TcKimlikNo,
                    Email = x.Email,
                    Telefon = x.PhoneNumber,

                    // Nullable (DateTime?) kontrolü
                    IseGirisTarihi = x.IseGirisTarihi.HasValue ? x.IseGirisTarihi.Value.ToString("dd.MM.yyyy") : "",
                    IstenAyrilisTarihi = x.IstenAyrilisTarihi.HasValue ? x.IstenAyrilisTarihi.Value.ToString("dd.MM.yyyy") : "Devam Ediyor",

                    Amir = !string.IsNullOrEmpty(x.AmirAdSoyad) ? x.AmirAdSoyad : "Yok",
                    Roller = x.Roller != null && x.Roller.Any() ? string.Join(", ", x.Roller) : "Personel",
                    CalismaDurumu = x.CalismaDurumu,
                    PersonelTipi = x.PersonelTipi,

                    Durum = x.SilindiMi ? "Pasif" : "Aktif"
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Departman Bazlý Personel");
            return BaseResult<byte[]>.Success(fileBytes);
        }
        // 3. Aktif Personeller Raporu (Sadece Çalýþmaya Devam Edenler)
        // 3. Aktif Personeller Raporu (Sadece Çalýþmaya Devam Edenler - A'dan Z'ye Tüm Bilgiler)
        public async Task<BaseResult<byte[]>> GetActivePassivePersonnelExcelAsync()
        {
            var result = await _userService.GetAllUserWithDepartmentAndUnitAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // SADECE AKTÝF PERSONELLERÝ FÝLTRELÝYORUZ (!x.SilindiMi ve Çýkýþ Tarihi Boþ Olanlar)
            var exportData = result.Data
                .Where(x => !x.SilindiMi && x.IstenAyrilisTarihi == null)
                .OrderBy(x => x.Ad)
                .Select(x => new
                {
                    Id = x.Id,
                    SicilNo = x.SicilNo,
                    Ad = x.Ad,
                    Soyad = x.Soyad,
                    TcKimlikNo = x.TcKimlikNo,
                    KullaniciAdi = x.UserName,
                    Email = x.Email,
                    Telefon = string.IsNullOrEmpty(x.PhoneNumber) ? "Belirtilmemiþ" : x.PhoneNumber,

                    // Nullable (DateTime?) olanlar için HasValue kontrolü
                    DogumTarihi = x.DogumTarihi.HasValue ? x.DogumTarihi.Value.ToString("dd.MM.yyyy") : "",
                    Cinsiyet = x.Cinsiyet,
                    MedeniDurum = x.MedeniDurum,
                    KanGrubu = x.KanGrubu,

                    Adres = x.Adres,
                    AcilDurumKisi = x.AcilDurumKisiAdSoyad,
                    AcilDurumTelefon = x.AcilDurumTelefonu,

                    // Ýþ Bilgileri
                    IseGirisTarihi = x.IseGirisTarihi.HasValue ? x.IseGirisTarihi.Value.ToString("dd.MM.yyyy") : "",
                    IstenAyrilisTarihi = "Devam Ediyor", // Aktif olduklarý için ayrýlýþ tarihi yok
                    CalismaDurumu = x.CalismaDurumu,
                    PersonelTipi = x.PersonelTipi,
                    SgkSicilNo = x.SgkSicilNo,

                    // Ýliþkiler
                    Departman = x.Departman != null ? x.Departman.Ad : "Atanmadý",
                    Birim = x.Birim != null ? x.Birim.Ad : "Atanmadý",
                    Amir = !string.IsNullOrEmpty(x.AmirAdSoyad) ? x.AmirAdSoyad : "Yok",
                    Roller = x.Roller != null && x.Roller.Any() ? string.Join(", ", x.Roller) : "Personel",

                    // Normal (DateTime) olanlar
                    OlusturulmaTarihi = x.OlusturulmaTarihi.ToString("dd.MM.yyyy HH:mm"),
                    GuncellenmeTarihi = x.GuncellenmeTarihi.ToString("dd.MM.yyyy HH:mm"),

                    Durum = "Aktif" // Filtrelediðimiz için hepsi aktif
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Aktif Personel Listesi");
            return BaseResult<byte[]>.Success(fileBytes);
        }

        // 4. Ýzin Raporlarý
        public async Task<BaseResult<byte[]>> GetPermissionReportExcelAsync()
        {
            var result = await _permissionService.GetAllPermissionWithUser();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // ÝÇ ÝÇE NESNELERÝ (Personel, IzinTuru) DÜZLEÞTÝRÝYORUZ VE ONAY DURUMLARINI METNE ÇEVÝRÝYORUZ
            var exportData = result.Data
                .OrderByDescending(x => x.BaslangicTarihi) // En güncel/yeni izinler en üstte çýksýn
                .Select(x => new
                {
                    // Personel Bilgileri
                    SicilNo = x.Personel != null ? x.Personel.SicilNo : "Belirtilmemiþ",
                    Personel = x.Personel != null ? $"{x.Personel.Ad} {x.Personel.Soyad}" : "Bilinmeyen Personel",

                    // Ýzin Türü (Eðer DTO'nda 'Ad' yoksa kendi propertine göre 'Aciklama' vs. yapabilirsin)
                    IzinTuru = x.IzinTuru != null ? x.IzinTuru.Ad : "Belirtilmemiþ",

                    // Tarihler
                    BaslangicTarihi = x.BaslangicTarihi != default ? x.BaslangicTarihi.ToString("dd.MM.yyyy") : "",
                    BitisTarihi = x.BitisTarihi != default ? x.BitisTarihi.ToString("dd.MM.yyyy") : "",

                    // ÝK'nýn çok iþine yarayacak bonus özellik: Otomatik gün hesabý (Ayný günse 1 sayar)
                    KullanilanGunSayisi = (x.BitisTarihi.Date - x.BaslangicTarihi.Date).Days > 0
                                          ? (x.BitisTarihi.Date - x.BaslangicTarihi.Date).Days
                                          : 1,

                    Aciklama = string.IsNullOrEmpty(x.Aciklama) ? "-" : x.Aciklama,

                    // Nullable bool (bool?) yönetimi: true = Onaylandý, false = Reddedildi, null = Bekliyor
                    AmirOnayi = x.AmirOnayi == true ? "Onaylandý" : (x.AmirOnayi == false ? "Reddedildi" : "Bekliyor"),
                    IkOnayi = x.IkOnayi == true ? "Onaylandý" : (x.IkOnayi == false ? "Reddedildi" : "Bekliyor"),

                    // Genel Durum Özeti Sütunu
                    GenelDurum = (x.AmirOnayi == true && x.IkOnayi == true) ? "Ýzin Onaylandý" :
                                 (x.AmirOnayi == false || x.IkOnayi == false) ? "Ýzin Reddedildi" : "Onay Sürecinde"
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Ýzin Raporlarý");
            return BaseResult<byte[]>.Success(fileBytes);
        }



        // 5. Eðitim Raporlarý
        public async Task<BaseResult<byte[]>> GetEducationReportExcelAsync()
        {
            var result = await _educationService.GetUserEducationWithAllInfoAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // ÝÇ ÝÇE NESNELERÝ (AppUser, Egitim) DÜZLEÞTÝRÝYORUZ
            var exportData = result.Data
                .OrderByDescending(x => x.BasvuruTarihi) // En yeni eðitim baþvurularý en üstte çýksýn
                .Select(x => new
                {
                    // Personel Bilgileri
                    SicilNo = x.AppUser != null && !string.IsNullOrEmpty(x.AppUser.SicilNo) ? x.AppUser.SicilNo : "Belirtilmemiþ",
                    Personel = x.AppUser != null ? $"{x.AppUser.Ad} {x.AppUser.Soyad}" : "Bilinmeyen Personel",

                    // Eðitim Bilgisi (Eðitim DTO'sunda isim tutan property 'Ad' olarak varsayýldý)
                    EgitimAdi = x.Egitim != null ? x.Egitim.Ad : "Belirtilmemiþ",

                    // Baþvuru Tarihi (Saatiyle birlikte görmek ÝK için önemlidir)
                    BasvuruTarihi = x.BasvuruTarihi != default ? x.BasvuruTarihi.ToString("dd.MM.yyyy HH:mm") : "",

                    // Enum olan durumu metne çeviriyoruz (Bekliyor, Onaylandi, Reddedildi vb.)
                    Durum = x.BasvuruDurumu.ToString(),

                    // ÝK / Admin açýklamasý
                    AdminAciklamasi = string.IsNullOrEmpty(x.AdminAciklamasi) ? "-" : x.AdminAciklamasi
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Eðitim Raporlarý");
            return BaseResult<byte[]>.Success(fileBytes);
        }



        // 6. Sertifika Raporlarý
        public async Task<BaseResult<byte[]>> GetCertificateReportExcelAsync()
        {
            var result = await _certificateService.GetAllAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // EXCEL'E AKTARILACAK VERÝLERÝ DÜZLEÞTÝRÝYOR VE FORMATLIYORUZ
            var exportData = result.Data
                .OrderByDescending(x => x.AlinmaTarihi) // En yeni alýnan sertifikalar en üstte çýksýn
                .Select(x => new
                {
                    // NOT: DTO'nda þu an AppUser ve SertifikaTuru nesneleri olmadýðý için sadece ID basýyoruz.
                    // Eðer Include edip DTO'ya eklersen burayý x.AppUser.Ad + " " + x.AppUser.Soyad olarak deðiþtirebilirsin!
                    PersonelId = x.AppUserId,
                    SertifikaTuruId = x.SertifikaTuruId,

                    BelgeNo = string.IsNullOrEmpty(x.BelgeNo) ? "-" : x.BelgeNo,
                    VerenKurum = string.IsNullOrEmpty(x.VerenKurum) ? "-" : x.VerenKurum,

                    // Tarihleri düzgün string formata çeviriyoruz
                    AlinmaTarihi = x.AlinmaTarihi != default ? x.AlinmaTarihi.ToString("dd.MM.yyyy") : "",
                    GecerlilikTarihi = x.GecerlilikTarihi != default ? x.GecerlilikTarihi.ToString("dd.MM.yyyy") : "",

                    // Eðer yenileme tarihi geçerlilik tarihiyle aynýysa (default), boþ býrakalým ki kafa karýþtýrmasýn
                    YenilemeTarihi = x.YenilemeTarihi != default && x.YenilemeTarihi != x.GecerlilikTarihi
                                     ? x.YenilemeTarihi.ToString("dd.MM.yyyy")
                                     : "-",

                    // CertificateStatus Enum'unu metne çeviriyoruz (Geçerli, Ýptal, Süresi Doldu vs.)
                    Durumu = x.Durumu.ToString(),

                    Aciklama = string.IsNullOrEmpty(x.Aciklama) ? "-" : x.Aciklama,

                    // Link basmak yerine sisteme dosya yüklenmiþ mi onu gösteriyoruz
                    DosyaDurumu = string.IsNullOrEmpty(x.DosyaYolu) ? "Dosya Yok" : "Yüklendi"
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Sertifika Raporlarý");
            return BaseResult<byte[]>.Success(fileBytes);
        }

        // 7. Zimmet Raporlarý
        public async Task<BaseResult<byte[]>> GetItemReportExcelAsync()
        {
            // EÐER ÝSÝMLERÝ GÖRMEK ÝSTERSEN ÝLERÝDE BURAYI ÞÖYLE DEÐÝÞTÝREBÝLÝRSÝN:
            // var result = await _itemService.GetAllItemsWithDetailsAsync();
            var result = await _itemService.GetAllAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // EXCEL'E AKTARILACAK VERÝLERÝ DÜZLEÞTÝRÝYOR VE FORMATLIYORUZ
            var exportData = result.Data
                .OrderByDescending(x => x.TeslimTarihi) // En son verilen zimmetler en üstte çýksýn
                .Select(x => new
                {
                    // Þimdilik DTO'nda sadece ID var. Detaylý servisi kullanýrsan burayý:
                    // Personel = x.AppUser.Ad + " " + x.AppUser.Soyad gibi deðiþtirebilirsin
                    PersonelId = x.AppUserId,
                    ZimmetTuruId = x.ZimmetTuruId,

                    SeriNumarasi = string.IsNullOrEmpty(x.SeriNumarasi) ? "-" : x.SeriNumarasi,

                    // Teslim Tarihini þýk formata çevir
                    TeslimTarihi = x.TeslimTarihi != default ? x.TeslimTarihi.ToString("dd.MM.yyyy") : "",

                    // ÝK için can alýcý nokta: Ýade tarihi boþsa "Kullanýmda" yazsýn
                    IadeTarihi = x.IadeTarihi.HasValue ? x.IadeTarihi.Value.ToString("dd.MM.yyyy") : "Kullanýmda (Ýade Edilmedi)",

                    // Enum olan Zimmet Durumu'nu metne çevir (Aktif, IadeEdildi, Arizali vs.)
                    Durumu = x.Durumu.ToString(),

                    Aciklama = string.IsNullOrEmpty(x.Aciklama) ? "-" : x.Aciklama
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Zimmet Raporlarý");
            return BaseResult<byte[]>.Success(fileBytes);
        }
        // 8. Vardiya Raporlarý
        public async Task<BaseResult<byte[]>> GetShiftReportExcelAsync()
        {
            var result = await _shiftService.GetAllAsync();
            if (!result.IsSuccessful || result.Data == null) return BaseResult<byte[]>.Fail("Veri bulunamadý.");

            // EXCEL'E AKTARILACAK VERÝLERÝ DÜZLEÞTÝRÝYOR VE FORMATLIYORUZ
            var exportData = result.Data
                .Select(x => new
                {
                    VardiyaAdi = string.IsNullOrEmpty(x.Aciklama) ? "-" : x.Aciklama,

                    // TimeSpan (Saat) deðerlerini saliselerden kurtarýp "hh:mm" formatýna çeviriyoruz
                    BaslangicSaati = x.BaslangicSaati.ToString(@"hh\:mm"),
                    BitisSaati = x.BitisSaati.ToString(@"hh\:mm"),

                    AraDinlenmeDk = x.AraDinlenmeSuresiDk,

                    // Net çalýþma süresini saat:dakika formatýnda veriyoruz
                    NetCalismaSuresi = x.CalismaSuresi.ToString(@"hh\:mm"),

                    // O vardiyada kaç kiþi olduðunu hýzlýca görebilmek için Sayý sütunu ekliyoruz
                    PersonelSayisi = x.Personeller != null ? x.Personeller.Count : 0,

                    // Ýç içe olan Personel listesini tek bir metin hücresine (Ad Soyad, Ad Soyad) çeviriyoruz
                    Personeller = x.Personeller != null && x.Personeller.Any()
                                  ? string.Join(", ", x.Personeller.Select(p => $"{p.Ad} {p.Soyad}"))
                                  : "Personel Atanmamýþ"
                }).ToList();

            var fileBytes = ExcelHelper.CreateExcel(exportData, "Vardiya Raporlarý");
            return BaseResult<byte[]>.Success(fileBytes);
        }
    }
}