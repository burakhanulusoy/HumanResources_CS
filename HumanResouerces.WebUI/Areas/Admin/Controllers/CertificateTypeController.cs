using HumanResources.Business.DTOs.CertificateTypeDtos;
using HumanResources.WebUI.Services.CertificateTypeServices;
using Microsoft.AspNetCore.Mvc;





namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateTypeController(ICertificateTypeService _certificateTypeService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _certificateTypeService.GetAllAsync();
            return View(response.Data);
        }

        public IActionResult CreateCertificateType()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificateType(CreateCertificateTypeDto createDto)
        {
            await _certificateTypeService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteCertificateType(int id)
        {
            await _certificateTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateCertificateType(int id)
        {
            var response = await _certificateTypeService.GetByIdAsync(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCertificateType(UpdateCertificateTypeDto updateDto)
        {
            await _certificateTypeService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TypeCertificates(int id)
        {
            var response = await _certificateTypeService.GetWithCertificatesAsync(id);
            return View(response.Data);
        }
    }
}