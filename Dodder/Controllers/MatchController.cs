using Dodder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Dodder.Controllers
{
    public class MatchController : Controller
    {
        PRN211Context db = new PRN211Context();
        public IActionResult Index()
        {

            Int32 UserId = (int)HttpContext.Session.GetInt32("id");
            ViewBag.ListUsers = db.UserAccounts
                .Where(u => u.Id != UserId && //khac chinh minh
                       db.UserDislikes.Where(d => d.UserAccountId == UserId && d.UserAccountIdDislike == u.Id).FirstOrDefault() == null &&  //user kia khong nam trong bang dislike va like cua user nay
                       db.UserLikes.Where(l => l.UserAccountId == UserId && l.UserAccountIdLike == u.Id).FirstOrDefault() == null).ToList();
            return View();
        }
        public JsonResult React(int id, bool love)
        {
            Int32 UserId = (int)HttpContext.Session.GetInt32("id");
            string notification = "";
            if (love)
            {
                db.UserLikes.Add(new UserLike() { UserAccountId = UserId, UserAccountIdLike = id });
                //Neu duoc liek
                if (db.UserLikes.Where(u => u.UserAccountId == id && u.UserAccountIdLike == UserId).FirstOrDefault() != null)
                {
                    notification = "Matched";
                }
            }
            else
            {
                db.UserDislikes.Add(new UserDislike() { UserAccountId = UserId, UserAccountIdDislike = id });
            }
            db.SaveChanges();
            return Json(notification);
        }
    }
}
