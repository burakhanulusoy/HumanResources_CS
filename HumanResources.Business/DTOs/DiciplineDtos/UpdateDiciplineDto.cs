using Microsoft.AspNetCore.Http;
using System;

namespace HumanResources.Business.DTOs.DiciplineDtos
{
    public class UpdateDiciplineDto
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string DisiplinNedeni { get; set; }
        public string Detay { get; set; }
        public DateTime OlayTarihi { get; set; }

        // Kullanýcýdan yeni bir dosya almak için:
        public IFormFile? Dosya { get; set; }
    }
}