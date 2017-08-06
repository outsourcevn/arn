using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aries.Models;
using PagedList;
namespace Aries.Controllers
{
    public class usersController : Controller
    {
        private etronEntities db = new etronEntities();

        // GET: users
        public ActionResult Index(int? page)
        {
            if (Config.getCookie("logged") == "") return RedirectToAction("Login", "Admin");
            using (var db = new etronEntities())
            {
                var users = db.users;
                var pageNumber = page ?? 1;
                var onePage = users.OrderBy(f => f.name).ToPagedList(pageNumber, 20);

                ViewBag.onePage = onePage;
            }
            return View();
        }
        [HttpPost]
        public string addUpdateUser(user u)
        {
            return DBContext.addUpdateUser(u);
        }

        [HttpPost]
        public string deleteUser(int uId)
        {
            return DBContext.deleteUser(uId);
        }

        public string validateExistInfo(string name)
        {
            try
            {
                using (var db = new etronEntities())
                {
                    user u = null;
                    if (!string.IsNullOrEmpty(name))
                    {
                        u = db.users.Where(f => f.name == name).FirstOrDefault();
                    }
                    if (u == null) return string.Empty;
                    return "Exist";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
