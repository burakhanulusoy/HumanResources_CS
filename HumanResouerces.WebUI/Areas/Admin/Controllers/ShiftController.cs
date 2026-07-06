using HumanResources.Business.DTOs.ShiftDtos;
using HumanResources.WebUI.Services.ShiftServices;
using HumanResources.WebUI.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShiftController(IShiftService _shiftService, IUserService _userService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await _shiftService.GetAllAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _shiftService.GetById2Async(id);
            return View(response.Data);
        }

        public async Task<IActionResult> ShiftListReport()
        {
            var response = await _shiftService.GetAllAsync();
            return View(response.Data);
        }

        public async Task<IActionResult> CreateShift()
        {
            var usersResponse = await _userService.GetAllAsync();
            var shiftsResponse = await _shiftService.GetAllAsync();

            ViewBag.Users = usersResponse.Data;

            // VardiyaId -> "08:00 - 16:00" formatında saat aralığı sözlüğü
            ViewBag.ShiftTimes = shiftsResponse.Data?
                .ToDictionary(
                    s => s.Id,
                    s => $"{s.BaslangicSaati:hh\\:mm} - {s.BitisSaati:hh\\:mm}"
                ) ?? new Dictionary<int, string>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateShift(CreateShiftDto createDto)
        {
            await _shiftService.CreateAsync(createDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UpdateShift(int id)
        {
            var shiftResponse = await _shiftService.GetById2Async(id);
            var usersResponse = await _userService.GetAllAsync();
            var shiftsResponse = await _shiftService.GetAllAsync();

            ViewBag.Users = usersResponse.Data;

            ViewBag.ShiftTimes = shiftsResponse.Data?
                .ToDictionary(
                    s => s.Id,
                    s => $"{s.BaslangicSaati:hh\\:mm} - {s.BitisSaati:hh\\:mm}"
                ) ?? new Dictionary<int, string>();

            var updateDto = new UpdateShiftDto
            {
                Id = shiftResponse.Data.Id,
                Aciklama = shiftResponse.Data.Aciklama,
                BaslangicSaati = shiftResponse.Data.BaslangicSaati,
                BitisSaati = shiftResponse.Data.BitisSaati,
                AraDinlenmeSuresiDk = shiftResponse.Data.AraDinlenmeSuresiDk,
                PersonelIds = shiftResponse.Data.Personeller?.Select(x => x.Id).ToList() ?? new List<int>()
            };
            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateShift(UpdateShiftDto updateDto)
        {
            await _shiftService.UpdateAsync(updateDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteShift(int id)
        {
            await _shiftService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}