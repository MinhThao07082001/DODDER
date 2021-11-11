using Dodder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Dodder.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        PRN211Context db = new PRN211Context();
        public IActionResult Dodder(int id)
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Match");
            }
        }
        public IActionResult Safety()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Match");
            }
        }
    }
}
