using HumanResources.Business.DTOs.ItemDtos;
using HumanResources.Business.Services.ItemServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController(IItemService _itemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _itemService.GetAllAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _itemService.GetByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDto item)
        {
            var response = await _itemService.CreateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _itemService.DeleteAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateItemDto item)
        {
            var response = await _itemService.UpdateAsync(item);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        // --- Özel Metotlar ---

        [HttpGet("GetAllWithDetails")]
        public async Task<IActionResult> GetAllWithDetails()
        {
            // Admin Paneli: Tüm zimmetleri, kime ait olduğu ve türüyle birlikte listeler
            var response = await _itemService.GetAllItemsWithDetailsAsync();
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            // Profil Sayfası: İlgili personelin üzerindeki zimmetleri getirir
            var response = await _itemService.GetItemsByUserIdAsync(userId);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("GetWithDetails/{id}")]
        public async Task<IActionResult> GetWithDetails(int id)
        {
            // Detay Sayfası: Tek bir eşyaya tıklanınca tüm özelliklerini ve kimde olduğunu getirir
            var response = await _itemService.GetItemWithDetailsByIdAsync(id);
            return response.IsSuccessful ? Ok(response) : BadRequest(response);
        }

        [HttpGet("ExcelExport")]
        public async Task<IActionResult> ExcelExport()
        {
            var response = await _itemService.GetAllItemsWithDetailsAsync();
            var list = response.Data ?? new List<HumanResources.Business.DTOs.ItemDtos.ItemDto>();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Zimmetler");

            // Başlıklar
            string[] headers =
            {
        "#", "Personel", "Sicil No", "Departman", "Birim", "Amir",
        "Kategori", "Marka", "Model", "Seri No",
        "Teslim Tarihi", "İade / Süre", "Durum", "Açıklama"
    };

            for (int c = 0; c < headers.Length; c++)
                ws.Cell(1, c + 1).Value = headers[c];

            var headerRange = ws.Range(1, 1, 1, headers.Length);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#0B2545");
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            int sira = 1;
            foreach (var z in list)
            {
                string personel = $"{z.AppUser?.Ad} {z.AppUser?.Soyad}".Trim();
                string iadeSure = z.SuresizMi
                    ? "Süresiz"
                    : (z.IadeTarihi.HasValue ? z.IadeTarihi.Value.ToString("dd.MM.yyyy") : "-");

                ws.Cell(row, 1).Value = sira++;
                ws.Cell(row, 2).Value = string.IsNullOrWhiteSpace(personel) ? "-" : personel;
                ws.Cell(row, 3).Value = z.AppUser?.SicilNo ?? "-";
                ws.Cell(row, 4).Value = z.AppUser?.DepartmanAd ?? "-";
                ws.Cell(row, 5).Value = z.AppUser?.BirimAd ?? "-";
                ws.Cell(row, 6).Value = z.AppUser?.AmirAdSoyad ?? "-";
                ws.Cell(row, 7).Value = z.Demirbas?.ZimmetTuru?.Ad ?? "-";
                ws.Cell(row, 8).Value = z.Demirbas?.Marka ?? "-";
                ws.Cell(row, 9).Value = z.Demirbas?.Model ?? "-";
                ws.Cell(row, 10).Value = string.IsNullOrEmpty(z.Demirbas?.SeriNumarasi) ? "-" : z.Demirbas.SeriNumarasi;
                ws.Cell(row, 11).Value = z.TeslimTarihi.ToString("dd.MM.yyyy");
                ws.Cell(row, 12).Value = iadeSure;
                ws.Cell(row, 13).Value = TranslateZimmetDurumu(z.Durumu.ToString());
                ws.Cell(row, 14).Value = string.IsNullOrEmpty(z.Aciklama) ? "-" : z.Aciklama;
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.RangeUsed()?.SetAutoFilter();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            string fileName = $"Zimmet_Kayitlari_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        private static string TranslateZimmetDurumu(string durum) => durum switch
        {
            "Aktif" => "Aktif",
            "IadeEdildi" => "İade Edildi",
            "Arizali" => "Arızalı",
            "Kayip" => "Kayıp",
            _ => durum
        };

    }
}