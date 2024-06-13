using Microsoft.AspNetCore.Mvc;

namespace Attractions.Controllers
{
    public class SaintPetersburgController : Controller
    {
        public IActionResult Index() 
            =>  View("/Views/City/SaintPetersburg/Index.cshtml");
        public IActionResult Hermitage() 
            => View("/Views/City/SaintPetersburg/Hermitage.cshtml");
    }
}