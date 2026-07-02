using HumanResources.Business.Services.ReportServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HumanResources.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController(IReportService _reportService) : ControllerBase
    {
        // Dosyanın Excel formatında inmesi için gereken standart Content-Type
        private const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        [HttpGet("personnel-list")]
        public async Task<IActionResult> DownloadPersonnelList()
        {
            var result = await _reportService.GetPersonnelListExcelAsync();

            // DÜZELTME: result.Messages yerine result.Errors kullanıldı
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"PersonelListesi_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("department-personnel")]
        public async Task<IActionResult> DownloadDepartmentPersonnel()
        {
            var result = await _reportService.GetDepartmentBasedPersonnelExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"DepartmanPersonel_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("active-passive-personnel")]
        public async Task<IActionResult> DownloadActivePassivePersonnel()
        {
            var result = await _reportService.GetActivePassivePersonnelExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"AktifPasifPersonel_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> DownloadPermissions()
        {
            var result = await _reportService.GetPermissionReportExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"IzinRaporu_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("educations")]
        public async Task<IActionResult> DownloadEducations()
        {
            var result = await _reportService.GetEducationReportExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"EgitimRaporu_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("certificates")]
        public async Task<IActionResult> DownloadCertificates()
        {
            var result = await _reportService.GetCertificateReportExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"SertifikaRaporu_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("items")]
        public async Task<IActionResult> DownloadItems()
        {
            var result = await _reportService.GetItemReportExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"ZimmetRaporu_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpGet("shifts")]
        public async Task<IActionResult> DownloadShifts()
        {
            var result = await _reportService.GetShiftReportExcelAsync();
            if (!result.IsSuccessful) return BadRequest(result.Errors);

            return File(result.Data, ExcelContentType, $"VardiyaRaporu_{DateTime.Now:yyyyMMdd}.xlsx");
        }
    }
}