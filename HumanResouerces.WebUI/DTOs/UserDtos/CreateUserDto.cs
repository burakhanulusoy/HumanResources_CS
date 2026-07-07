namespace HumanResources.WebUI.DTOs.UserDtos
{
    public class CreateUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }

        public string? SicilNo { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }
        public string? TcKimlikNo { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public string? Cinsiyet { get; set; }
        public string? MedeniDurum { get; set; }
        public string? KanGrubu { get; set; }

        public IFormFile? Fotograf { get; set; }

        public string? Adres { get; set; }
        public string? AcilDurumKisiAdSoyad { get; set; }
        public string? AcilDurumTelefonu { get; set; }

        // Ýþ ve Organizasyon Bilgileri
        public int? DepartmanId { get; set; }
        public int? BirimId { get; set; }
        public int? AmirId { get; set; }

        public int? VardiyaId { get; set; }

        public string? CalismaDurumu { get; set; }
        public string? PersonelTipi { get; set; }
        public string? SgkSicilNo { get; set; }
    }
}