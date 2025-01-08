using Microsoft.AspNetCore.Mvc;
using Attractions.Models.dtoModels;
using Attractions.Dbcontext;
using System.Diagnostics;
using Attractions.Models;
using Attractions.Models.dboModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Attractions.Controllers
{
    public class MoscowController : Controller
    {

        private const string KeyId = "Id";
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _context;
        public MoscowController(ILogger<HomeController> logger, dbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
            => View("/Views/City/Moscow/Index.cshtml");
        
        public async Task<IActionResult> RedSquare()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if(id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 7).ToListAsync();

            return View("/Views/City/Moscow/RedSquare.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult RedSquare(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("RedSquare");
        }
        public async Task<IActionResult> BolshoiTheater()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 8).ToListAsync();
            return View("/Views/City/Moscow/BolshoiTheater.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult BolshoiTheater(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("BolshoiTheater");
        }

        public async Task<IActionResult> VDNH()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 9).ToListAsync();
            return View("/Views/City/Moscow/VDNH.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult VDNH(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("VDNH");
        }

        public async Task<IActionResult> MoscowCity()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 10).ToListAsync();
            return View("/Views/City/Moscow/MoscowCity.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult MoscowCity(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("MoscowCity");
        }

        public async Task<IActionResult> MoscowZoo()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 11).ToListAsync();
            return View("/Views/City/Moscow/MoscowZoo.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult MoscowZoo(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("MoscowZoo");
        }
        public async Task<IActionResult> MoscowMetro()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 12).ToListAsync();
            return View("/Views/City/Moscow/MoscowMetro.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult MoscowMetro(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("MoscowMetro");
        }
        public void AddFeedbackToDataBase(dtoFeedback feedback)
        {
            if (feedback.NameSender != null)
            {
                _context.Feedback.Add(GetFullFeedback(feedback));
                _context.SaveChanges();
            }
        }
        public Feedback GetFullFeedback(dtoFeedback feedback)
            => new Feedback
            {
                Id_Sight = feedback.Id_Sight,
                NameSender = feedback.NameSender,
                FeedBackText = feedback.FeedBackText,
                Ball = feedback.Ball,
                Id_User = int.TryParse(Request.Cookies[KeyId], out var tempUserID) ? tempUserID : null,
                fb_datatime = DateTime.UtcNow,
            };
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
