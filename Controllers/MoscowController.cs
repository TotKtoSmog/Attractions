﻿using Microsoft.AspNetCore.Mvc;
using Attractions.Models.dtoModels;
using Attractions.Dbcontext;
using System.Diagnostics;
using Attractions.Models;
using Attractions.Models.dboModels;
using Microsoft.EntityFrameworkCore;

namespace Attractions.Controllers
{
    public class MoscowController : Controller
    {
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
            List<Feedback> feedbacks = await _context.Feedback.Where(f => f.IsAccepted && f.Id_Sight == 8).ToListAsync();
            return View("/Views/City/Moscow/BolshoiTheater.cshtml", feedbacks);
        }
        [HttpPost]
        public IActionResult BolshoiTheater(dtoFeedback feedback)
        {
            AddFeedbackToDataBase(feedback);
            return RedirectToAction("BolshoiTheater");
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
                fb_datatime = DateTime.UtcNow,
            };
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}