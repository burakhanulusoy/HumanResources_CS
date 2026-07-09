using HumanResources.Business.DTOs.DemirbasDtos;
using HumanResources.Business.Services.DemirbasServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ClosedXML.Excel;



namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemirbaslarController(IDemirbasService _demirbasService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var r = await _demirbasService.GetAllAsync();
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var r = await _demirbasService.GetByIdAsync(id);
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDemirbasDto dto)
        {
            var r = await _demirbasService.CreateAsync(dto);
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateDemirbasDto dto)
        {
            var r = await _demirbasService.UpdateAsync(dto);
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var r = await _demirbasService.DeleteAsync(id);
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        // --- Özel ---
        [HttpGet("GetAllWithType")]
        public async Task<IActionResult> GetAllWithType()
        {
            var r = await _demirbasService.GetAllWithTypeAsync();
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpGet("GetAvailable")]   // Zimmet formu dropdown'u -> sadece müsaitler
        public async Task<IActionResult> GetAvailable()
        {
            var r = await _demirbasService.GetAvailableAsync();
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpGet("GetWithType/{id}")]
        public async Task<IActionResult> GetWithType(int id)
        {
            var r = await _demirbasService.GetWithTypeByIdAsync(id);
            return r.IsSuccessful ? Ok(r) : BadRequest(r);
        }

        [HttpGet("ExcelExport")]
        public async Task<IActionResult> ExcelExport()
        {
            var response = await _demirbasService.GetAllWithTypeAsync();
            var list = response.Data ?? new List<HumanResources.Business.DTOs.DemirbasDtos.DemirbasDto>();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Demirbaslar");

            // Başlık satırı
            ws.Cell(1, 1).Value = "#";
            ws.Cell(1, 2).Value = "Kategori";
            ws.Cell(1, 3).Value = "Marka";
            ws.Cell(1, 4).Value = "Model";
            ws.Cell(1, 5).Value = "Seri No";
            ws.Cell(1, 6).Value = "Durum";
            ws.Cell(1, 7).Value = "Açıklama";

            var headerRange = ws.Range(1, 1, 1, 7);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.FromHtml("#0B2545");
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            int sira = 1;
            foreach (var d in list)
            {
                ws.Cell(row, 1).Value = sira++;
                ws.Cell(row, 2).Value = d.ZimmetTuru?.Ad ?? "-";
                ws.Cell(row, 3).Value = d.Marka ?? "-";
                ws.Cell(row, 4).Value = d.Model ?? "-";
                ws.Cell(row, 5).Value = string.IsNullOrEmpty(d.SeriNumarasi) ? "-" : d.SeriNumarasi;
                ws.Cell(row, 6).Value = TranslateDemirbasDurumu(d.Durumu.ToString());
                ws.Cell(row, 7).Value = string.IsNullOrEmpty(d.Aciklama) ? "-" : d.Aciklama;
                row++;
            }

            ws.Columns().AdjustToContents();
            ws.RangeUsed()?.SetAutoFilter();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            string fileName = $"Demirbas_Listesi_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
            return File(content,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }

        private static string TranslateDemirbasDurumu(string durum) => durum switch
        {
            "Musait" => "Müsait",
            "Zimmetli" => "Zimmetli",
            "Arizali" => "Arızalı",
            "HizmetDisi" => "Hizmet Dışı",
            _ => durum
        };

    }
}