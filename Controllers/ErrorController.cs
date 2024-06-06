using Attractions.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Attractions.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404) return View("404");
            }

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
