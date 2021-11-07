using Dodder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
        public IActionResult Dodder()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Safety()
        {
            return View();
        }
    }
}
