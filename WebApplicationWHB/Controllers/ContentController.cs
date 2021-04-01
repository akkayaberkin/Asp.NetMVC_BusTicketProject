using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplicationWHB.Models;

namespace WebApplicationWHB.Controllers
{
    public class ContentController : Controller
    {
        // GET: Content
        public ActionResult SearchBus(FormCollection frm)
        {
            AccountsDbEntities10 db = new AccountsDbEntities10();


            List<SelectListItem> list = (from i in db.startCities.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.cityName,
                                             Value = i.idcity.ToString()
                                         }).ToList();

            ViewBag.fssc = list;


            List<SelectListItem> degerler = (from i in db.lastCities.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.cityName,
                                                 Value = i.idCity.ToString()
                                             }

                                            ).ToList();
            ViewBag.dgr = degerler;
            List<SelectListItem> tarih = (from i in db.busDates.ToList()
                                          select new SelectListItem
                                          {
                                              Text = i.time,
                                              Value = i.id.ToString()
                                          }
                                        ).ToList();

            ViewBag.trh = tarih;

            List<SelectListItem> saat = (from i in db.busTimes.ToList()
                                         select new SelectListItem
                                         {
                                             Text = i.travelTime,
                                             Value = i.id.ToString()
                                         }
                                        ).ToList();
            ViewBag.time = saat;
            //List<SelectListItem> koltuklist = (from i in db.OrdersTables.ToList()
            //                             select new SelectListItem
            //                             {

            //                                 Text = i.koltukid.ToString(),
            //                                 Value = i.koltukid.ToString()
            //                             }

            //                            ).ToList();
            //ViewBag.koltuklist = koltuklist;

            //Koltukların id sini databaseten aldığımız ve ekrana bastığımız kısım 



            // OrderModel ordrmdl = new OrderModel();


            //ordrmdl.orders = db.OrdersTables.ToList<OrdersTable>();

            return View();


        }

        [HttpPost]
        public ActionResult CreateTicket(startCity startCity, lastCity lastCity, OrdersTable ordersTable, busDate busDate, busTime busTime, ankbu ankBu)
        {
            if (ankBu.PassengerName==null)
            {
                ankBu.PassengerName = "Belirtilmemiş İsim";
               
            }
            if (ankBu.PassengerEmail == null)
            {
                ankBu.PassengerEmail = "Belirtilmemiş Mail";

            }
            if (startCity.idcity.ToString() == "34")
            {
                startCity.cityName = "İstanbul";
            }
            if (lastCity.idCity.ToString() == "1")
            {
                lastCity.cityName = "Ankara";

            }
            else if (lastCity.idCity.ToString() == "2")
            {
                lastCity.cityName = "İzmir";
            }
            else if (lastCity.idCity.ToString() == "3")
            {
                lastCity.cityName = "Eskişehir";
            }
            if (busTime.id.ToString() == "1")
            {
                busTime.travelTime = "12:00";
            }
            else if (busTime.id.ToString() == "2")
            {
                busTime.travelTime = "17:00";
            }
            if (busDate.id.ToString() == "1")
            {
                busDate.time = "12/04/2021";

            }
            else if (busDate.id.ToString() == "2") { busDate.time = "13/04/2021"; }
            AccountsDbEntities10 db = new AccountsDbEntities10();
            Table tbl = new Table();
            ordersTable.binis = startCity.cityName;
            ordersTable.inis = lastCity.cityName;
            ordersTable.koltukid = ordersTable.koltukid + 1;
            ordersTable.date = busDate.time;
            ordersTable.name = ankBu.PassengerName;
            ordersTable.email = ankBu.PassengerEmail;

            db.OrdersTables.Add(ordersTable);
            db.SaveChanges();

            //List<string> list = new List<string>();

            //list.Add(startCity.cityName);
            //list.Add(lastCity.cityName);
            //list.Add(ordersTable.date);   //boş
            //list.Add(busDate.time);
            //list.Add(busTime.travelTime); //boş 

            return RedirectToAction("SearchBus", "Content");
        }
        public ActionResult Manager()
        {
            return View();

        }
        
        public ActionResult ManagerIndex(Manager manager)
        {
            using (AccountsDbEntities10 db = new AccountsDbEntities10())
            {

                var userdetails = db.Tables.Where(x => x.Name == manager.Name && x.Password == manager.Password).FirstOrDefault();
                if (manager.Name == "Admin" && manager.Password == "Amerika123")
                {

                    Session["userName"] = userdetails.Name;
                    ViewBag.Name = manager.Name;
                    return View(db.OrdersTables.ToList());

                }
                return View(db.OrdersTables.ToList());
            }
           
        }


    }
}