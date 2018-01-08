using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RoutingWebbApp.Models;
using Microsoft.AspNetCore.Authorization;
using RoutingWebbApp.Services;

namespace RoutingWebbApp.Controllers
{
    public class HomeController : Controller
    {
        private IEmailSender emailSender;
        public HomeController(IEmailSender _emailSender)
        {
            emailSender = _emailSender;
        }

        public async Task<IActionResult> Index()
        {
            await emailSender.SendEmailAsync("test@test.com", "Index was executed", "I am not happy");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles="Admin")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
