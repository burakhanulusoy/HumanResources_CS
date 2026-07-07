using HumanResources.WebUI.DTOs.DiciplineDtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanResouerces.WebUI.Models
{
    // ViewBag yerine tip güvenli ViewModel'ler.
    // "as List<...>" cast'i null dönerse dropdown sessizce boş kalıyordu;
    // ViewModel ile bu hata derleme zamanında yakalanır.

    public class DiciplineCreateViewModel
    {
        public CreateDiciplineDto Record { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();
    }

    public class DiciplineUpdateViewModel
    {
        public UpdateDiciplineDto Record { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();

        // Mevcut belge var mı bilgisini view'da gösterebilmek için
        public string? MevcutDosyaYolu { get; set; }
    }

    public class DiciplineDetailViewModel
    {
        public DiciplineDto CurrentRecord { get; set; }
        public List<DiciplineDto> OtherRecords { get; set; } = new();
    }
}
