using HumanResouerces.WebUI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HumanResouerces.WebUI.Filters
{
    public class ValidationExceptionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Bir şey yapmıyoruz; iş bittikten sonra devreye giriyoruz
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not ApiValidationException validationException)
                return;

            // ActionExecutedContext.Controller MEVCUT — dolu ViewData burada
            if (context.Controller is not Controller controller)
                return;

            foreach (var error in validationException.Errors)
                controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

            var actionName = context.RouteData.Values["action"]?.ToString();

            context.Result = new ViewResult
            {
                ViewName = actionName,
                ViewData = controller.ViewData,   // ← dropdown'lar (ViewBag) + ModelState korunuyor
                TempData = controller.TempData
            };

            context.ExceptionHandled = true;
        }
    }
}