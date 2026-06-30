using HumanResources.Entity.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace HumanResources.Entity.Entities
{
    public class AppUser : IdentityUser<int>, IAuditEntity
    {


        // Kimlik Bilgileri
        public string SicilNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TcKimlikNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Cinsiyet { get; set; }
        public string MedeniDurum { get; set; }
        public string KanGrubu { get; set; }
        public string FotografUrl { get; set; }

        // Ek Ýletiþim Bilgileri
        public string Adres { get; set; }
        public string AcilDurumKisiAdSoyad { get; set; }
        public string AcilDurumTelefonu { get; set; }

        // Ýþ ve Organizasyon Bilgileri (Guid yerine int kullanýldý)
        public int DepartmanId { get; set; }
        public Departman Departman { get; set; }

        public int BirimId { get; set; }
        public Birim Birim { get; set; }


        public int? AmirId { get; set; }
        public AppUser Amir { get; set; }
        public IList<AppUser> BagliPersoneller { get; set; }

        public IList<Izin> Izinler { get; set; }

        public DateTime IseGirisTarihi { get; set; }
        public DateTime? IstenAyrilisTarihi { get; set; }
        public string CalismaDurumu { get; set; }
        public string PersonelTipi { get; set; }
        public string SgkSicilNo { get; set; }


        public int? VardiyaId { get; set; }
        public Vardiya? Vardiya { get; set; }


        // Log ve Soft Delete
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime GuncellenmeTarihi { get; set; }
        public bool SilindiMi { get; set; } 


    }
}
