﻿using Microsoft.AspNetCore.Mvc;
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
        private const string KeyId = "Id";
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
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 1).ToListAsync();
            return View("/Views/City/SaintPetersburg/Hermitage.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult Hermitage(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return  RedirectToAction("Hermitage");
        }
        public async Task<IActionResult> IsaacCathedral()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 2).ToListAsync();
            return View("/Views/City/SaintPetersburg/IsaacCathedral.cshtml", feedbacks);
        }

        [HttpPost]
        public IActionResult IsaacCathedral(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("IsaacCathedral");
        }

        public async Task<IActionResult> PeterPavelFortress()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 3).ToListAsync();
            return View("/Views/City/SaintPetersburg/PeterPavelFortress.cshtml", feedbacks);
        }

        [HttpPost]
        public IActionResult PeterPavelFortress(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("PeterPavelFortress");
        }

        public async Task<IActionResult> SmolnyCathedral()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 4).ToListAsync();
            return View("/Views/City/SaintPetersburg/SmolnyCathedral.cshtml", feedbacks);
        }

        [HttpPost]
        public IActionResult SmolnyCathedral(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("SmolnyCathedral");
        }

        public async Task<IActionResult> SummerGarden()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 5).ToListAsync();
            return View("/Views/City/SaintPetersburg/SummerGarden.cshtml", feedbacks);
        }

        [HttpPost]
        public IActionResult CruiserAurora(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("CruiserAurora");
        }

        public async Task<IActionResult> CruiserAurora()
        {
            string id = Request.Cookies[KeyId] ?? "";
            if (id != "")
            {
                int UserId = Convert.ToInt32(id);
                User? u = await _context.Users.FindAsync(UserId);
                ViewData["FIO"] = $"{u.FirstName} {u.LastName}";
            }
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 6).ToListAsync();
            return View("/Views/City/SaintPetersburg/CruiserAurora.cshtml", feedbacks);
        }

        [HttpPost]
        public IActionResult SummerGarden(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("SummerGarden");
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