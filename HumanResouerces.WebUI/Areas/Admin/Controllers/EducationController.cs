using Microsoft.AspNetCore.Mvc;

namespace HumanResouerces.WebUI.Areas.Admin.Controllers
{
    public class EducationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
