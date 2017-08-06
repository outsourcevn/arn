using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aries.Models;
namespace Aries.Controllers
{
    public class HomeController : Controller
    {
        private etronEntities db = new etronEntities();
        public ActionResult Index()
        {
            try
            {
                var news = db.news.OrderByDescending(o => o.id).Take(3).ToList();
                ViewBag.news = news;
            }
            catch
            {

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Solution()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}