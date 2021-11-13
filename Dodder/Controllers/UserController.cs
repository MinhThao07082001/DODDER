using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodder.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Dodder.Program;
using WebMatrix.WebData;
using static Dodder.Controllers.UserController;

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
                updatePosition(obj, user.Latitude, user.Longitude);
                HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(user));
                HttpContext.Session.SetInt32("id", obj.Id);
                return RedirectToAction("Index", "Match");
            }
            else
            {
                TempData["error"] = "Error";
                return View(user);
            }
        }
        private void updatePosition(UserAccount user, double lat, double longtitute)
        {
            var client = new RestClient("http://api.positionstack.com/v1/reverse?access_key=a39ca257b5ced55aac7e10b3ffdbf419&query=" + lat + "," + longtitute);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            dynamic res = JsonConvert.DeserializeObject(response.Content);
            dynamic item = res.data[0];
            try
            {
                user.Latitude = item.latitude;
                user.Longitude = item.longitude;
                user.Address = item.name + ", " + item.county + ", " + item.region;
            }
            catch (Exception)
            {
                user.Latitude = 0;
                user.Longitude = 0;
            }
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));
            db.UserAccounts.Update(user);
            db.SaveChanges();
        }
        public IActionResult Logout()
        {
            //if (HttpContext.Session.GetString("UserSession") != null)
            //{
            HttpContext.Session.Remove("UserSession");
            return RedirectToAction("Login");
            //}

        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }

        private class NewPassword { public int CodeSend { get; set; } }

        [HttpPost]
        public IActionResult ForgotPassword(UserAccount user)
        {
            Random oRandom = new Random();
            NewPassword newPassword = new NewPassword();
            newPassword.CodeSend = oRandom.Next(10000000, 99999999);
            using (PRN211Context db = new PRN211Context())
            {
                if (db.UserAccounts.Where(x => x.Email == user.Email).FirstOrDefault() != null)
                {
                    SendEmail sendEmail = new SendEmail();
                    string subject = "RESET YOUR ACCOUNT DODDER";
                    string content = "Your New Password Is : ";
                    bool checkSendMail = sendEmail.SendEmailNewpassword(user.Email, subject, content, newPassword.CodeSend);
                    if (checkSendMail == true)
                    {
                        int Id = db.UserAccounts.FirstOrDefault(t => t.Email.Contains(user.Email)).Id;
                        var FindUserByID = db.UserAccounts.Find(Id);
                        FindUserByID.Password = newPassword.CodeSend.ToString();
                        db.SaveChanges();
                        return View("Login");
                    }
                    else
                    {
                        TempData["error"] = "Error";
                        return View("ForgotPassword");
                    }
                }
                else
                {
                    TempData["error"] = "Error";
                    return View("ForgotPassword");
                }
            }
        }





    }
}
