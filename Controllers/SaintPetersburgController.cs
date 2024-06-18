using Microsoft.AspNetCore.Mvc;
using Attractions.Models.dtoModels;
using Attractions.Dbcontext;
using System.Diagnostics;
using Attractions.Models;
using Attractions.Models.dboModels;
using Microsoft.EntityFrameworkCore;

namespace Attractions.Controllers
{
    public class SaintPetersburgController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _context;
        public SaintPetersburgController(ILogger<HomeController> logger, dbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index() 
            =>  View("/Views/City/SaintPetersburg/Index.cshtml");
        public async Task<IActionResult> Hermitage()
        {
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 1).ToListAsync();
            return View("/Views/City/SaintPetersburg/Hermitage.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult Hermitage(dtoFeedback feedback)
        {
            if(feedback.NameSender != null)
            {
                _context.Feedback.Add(
                    new Models.dboModels.Feedback
                    {
                        Id_Sight = feedback.Id_Sight,
                        NameSender = feedback.NameSender,
                        FeedBackText = feedback.FeedBackText,
                        Ball = feedback.Ball
                    }
                    );
                _context.SaveChanges();
            }
            return View("/Views/City/SaintPetersburg/Hermitage.cshtml");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

    }
}