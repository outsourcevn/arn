using Aries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aries.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        public ActionResult Login()
        {
            //if (Config.getCookie("logged") != "") return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            user u = DBContext.validateLogin(model);
            if (u == null)
            {
                ModelState.AddModelError("error", "Bạn đã nhập sai Tên hoặc Mật Khẩu, vui lòng thử lại!");
                return View(model);
            }
            Session["user"] = u;
            return RedirectToAction("Index","News");
        }

        [HttpPost]
        public string validateLogin(LoginModel model)
        {
            try
            {
                user u = DBContext.validateLogin(model);
                if (u == null)
                {
                    return "Bạn đã nhập sai Tên hoặc Mật Khẩu, vui lòng thử lại!";
                }
                Config.setCookie("logged", u.name);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "Lỗi: " + ex.Message;
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Config.setCookie("logged", "");
            return RedirectToAction("Login");
        }
    }
}