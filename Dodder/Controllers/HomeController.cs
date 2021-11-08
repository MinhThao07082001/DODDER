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
