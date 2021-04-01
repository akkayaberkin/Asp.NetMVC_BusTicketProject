using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationWHB.Models;

namespace WebApplicationWHB.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home


            public ActionResult Index() { return View(); }
        public ActionResult IndexLogin()
        {


            return View();
        }
        public ActionResult Registerpage(int id = 0)
        {
            Table table = new Table();

            return View(table);

        }
        [HttpPost]
        public ActionResult Registerpage(Table table)
        {
            using (AccountsDbEntities10 db = new AccountsDbEntities10())

            {
                if (db.Tables.Any(x => x.Email == table.Email)||table.Name==null||table.Password==null||table.Email==null)
                { ViewBag.DublicateMessage = "Hata."; }

                else
                {
                    db.Tables.Add(table);
                    db.SaveChanges();


                    ModelState.Clear();
                    ViewBag.SuccesMessage = "Tamamlandı.";
                }
            }
            return View("Registerpage", new Table());

        }
        [HttpPost]
        public ActionResult Autherize(Table table)
        {
            using (AccountsDbEntities10 db = new AccountsDbEntities10())
            {
                var userdetails = db.Tables.Where(x => x.Name == table.Name && x.Password == table.Password).FirstOrDefault();


                if (userdetails == null)
                {
                    table.LoginErrorMessage = "Yanlış Kullanıcı Adı veya Şifre";
                    return View("IndexLogin", table);
                }

                else
                {
                    Session["userID"] = userdetails.Id;
                    Session["userName"] = userdetails.Name;
                    return RedirectToAction("SearchBus", "Content");
                }

            }
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("IndexLogin", "Home");
        }

    }
}