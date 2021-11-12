using Dodder.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Dodder.Controllers
{
    public class MatchController : Controller
    {
        PRN211Context db = new PRN211Context();
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                TempData["user"] = JsonConvert.DeserializeObject<UserAccount>(HttpContext.Session.GetString("User"));
                Int32 UserId = (int)HttpContext.Session.GetInt32("id");
                List<Conversation> conversations = db.Conversations.Where(c => c.UserAccountIdCreator == UserId || c.UserAccountId2 == UserId).ToList();
                ViewBag.ListMessage = conversations;
                ViewBag.UserId = UserId;
                ViewBag.ListUsers = db.UserAccounts
                    .Where(u => u.Id != UserId && //khac chinh minh
                           db.UserDislikes.Where(d => d.UserAccountId == UserId && d.UserAccountIdDislike == u.Id).FirstOrDefault() == null &&  //user kia khong nam trong bang dislike va like cua user nay
                           db.UserLikes.Where(l => l.UserAccountId == UserId && l.UserAccountIdLike == u.Id).FirstOrDefault() == null).ToList();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        public JsonResult React(int id, bool love)
        {
            Int32 UserId = (int)HttpContext.Session.GetInt32("id");
            string notification = "";
            if (love)
            {
                if (isOverLike(UserId))
                {
                    notification = "OverLike";
                }
                else
                {
                    db.UserLikes.Add(new UserLike() { UserAccountId = UserId, UserAccountIdLike = id });
                    //Neu duoc liek
                    if (db.UserLikes.Where(u => u.UserAccountId == id && u.UserAccountIdLike == UserId).FirstOrDefault() != null)
                    {
                        notification = "Matched";
                        //tao phong chat
                        db.Conversations.Add(new Conversation() { UserAccountIdCreator = UserId, UserAccountId2 = id });
                    }
                }

            }
            else
            {
                db.UserDislikes.Add(new UserDislike() { UserAccountId = UserId, UserAccountIdDislike = id });
            }
            db.SaveChanges();
            return Json(notification);
        }
        public IActionResult Message(int id = 0)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                TempData["user"] = JsonConvert.DeserializeObject<UserAccount>(HttpContext.Session.GetString("User"));
                Int32 UserId = (int)HttpContext.Session.GetInt32("id");
                //neu id =0 chi hien thi ra list danh sach nguoi nhan tin
                //lay danh sach phong
                List<Conversation> conversations = db.Conversations.Where(c => c.UserAccountIdCreator == UserId || c.UserAccountId2 == UserId).ToList();
                ViewBag.ListMessage = conversations;
                ViewBag.UserId = UserId;
                ViewBag.toId = id;
                if (id > 0)
                {
                    Conversation conversation = db.Conversations.Where(c => c.UserAccountIdCreator == UserId && c.UserAccountId2 == id ||
                    c.UserAccountIdCreator == id && c.UserAccountId2 == UserId).FirstOrDefault();
                    if (conversation != null)
                    {
                        ViewBag.ConversationID = conversation.Id;
                        ViewBag.UserID = UserId;
                        ViewBag.Messages = db.Messages.Where(m => m.ConversationId == conversation.Id).ToList();
                    }
                }
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        bool isOverLike(int UserId)
        {
            int count = 0;
            foreach (var item in db.UserLikes.ToList())
            {
                DateTime d1 = (DateTime)DateTime.Now;
                if (item.UserAccountId == UserId && d1.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    count++;
                }
            }
            return count >= 10;
        }
    }
}
