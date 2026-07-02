using Microsoft.AspNetCore.Http;
using System;

namespace HumanResources.Business.DTOs.UserDtos
{
    public class UpdateUserDto
    {
        public int? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
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

        // DEÐÝÞÝKLÝK: Güncelleme sýrasýnda yeni bir fotoðraf seçilirse diye eklendi
        public IFormFile? Fotograf { get; set; }

        // Ek Ýletiþim Bilgileri
        public string? Adres { get; set; }
        public string? AcilDurumKisiAdSoyad { get; set; }
        public string? AcilDurumTelefonu { get; set; }

        // Ýþ ve Organizasyon Bilgileri
        public int? DepartmanId { get; set; }
        public int? BirimId { get; set; }
        public int? AmirId { get; set; }

        // DEÐÝÞÝKLÝK: Personelin vardiyasý deðiþebilir diye eklendi
        public int? VardiyaId { get; set; }

        public string? CalismaDurumu { get; set; }
        public string? PersonelTipi { get; set; }
        public string? SgkSicilNo { get; set; }
    }
}