using HumanResources.WebUI.DTOs.DepartmentDtos;
using HumanResources.WebUI.DTOs.UnitDtos;

namespace HumanResources.WebUI.DTOs.UserDtos
{
    public class ResultUserDto 
    {
        // IdentityUser'dan Gelen Temel Özellikler
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        // Kimlik Bilgileri
        public string? SicilNo { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? TcKimlikNo { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? Cinsiyet { get; set; }
        public string? MedeniDurum { get; set; }
        public string? KanGrubu { get; set; }
        public string? FotografUrl { get; set; }

        // Ek Ýletiþim Bilgileri
        public string? Adres { get; set; }
        public string? AcilDurumKisiAdSoyad { get; set; }
        public string? AcilDurumTelefonu { get; set; }

        // Ýþ Bilgileri (Sadece Baðýmsýz Olanlar)
        public DateTime? IseGirisTarihi { get; set; }
        public DateTime? IstenAyrilisTarihi { get; set; }
        public string? CalismaDurumu { get; set; }
        public string? PersonelTipi { get; set; }
        public string? SgkSicilNo { get; set; }


        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
        public bool SilindiMi { get; set; }

        //iliþkiler
        public int DepartmanId { get; set; }
        public DepartmentDto Departman { get; set; }

        public int BirimId { get; set; }
        public UnitDto Birim { get; set; }

        // ResultUserDto içinde sadece "Amir"in kim olduðunu tut
        public int? AmirId { get; set; }
        public string? AmirAdSoyad { get; set; } // Sadece ekranda göstermek için adýný tut yeter
                                                 // Diðer özelliklerin altýna ekle:
        public IList<string> Roller { get; set; }

        public int? VardiyaId { get; set; }
        public string? VardiyaAciklama { get; set; }
    }
}