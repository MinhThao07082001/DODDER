using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodder.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dodder.Controllers
{
    public class UserController : Controller
    {
        PRN211Context db = new PRN211Context();
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                TempData["user"] = JsonConvert.DeserializeObject<UserAccount>(HttpContext.Session.GetString("User"));
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Genders = db.Genders.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserAccount user)
        {
            var obj = db.UserAccounts.Where(x => x.Email.Equals(user.Email) || x.Phone.Equals(user.Phone)).FirstOrDefault();
            if (obj == null)
            {
                db.UserAccounts.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Genders = db.Genders.ToList();
                TempData["error"] = "Error";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserAccount user = new UserAccount();
            return View(user);
        }
        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            var obj = db.UserAccounts.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
            if (obj != null)
            {
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(obj));
                HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetInt32("id", obj.Id);
                return RedirectToAction("Index", "Match");
            }else
            {
                TempData["error"] = "Error";
                return View(user);
            }
        }
        public IActionResult Logout()
        {
            //if (HttpContext.Session.GetString("UserSession") != null)
            //{
            HttpContext.Session.Remove("UserSession");
            return RedirectToAction("Login");
            //}

        }
    }
}
