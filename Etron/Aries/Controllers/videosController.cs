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
    public class videosController : Controller
    {
        private etronEntities db = new etronEntities();

        // GET: videos
        public ActionResult Index(int? pg, string search)
        {
            if (Config.getCookie("logged") == "") return RedirectToAction("Login", "Admin");
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;

            var data = (from q in db.videos select q);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.name.Contains(search));
                ViewBag.search = search;
            }

            data = data.OrderByDescending(x => x.id);

            return View(data.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult List(int? pg, string search, int? cat_id)
        {
            int pageSize = 25;
            if (pg == null) pg = 1;
            int pageNumber = (pg ?? 1);
            ViewBag.pg = pg;

            var data = (from q in db.videos select q);
            if (data == null)
            {
                return View(data);
            }
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                data = data.Where(x => x.name.Contains(search));
                ViewBag.search = search;
            }
            if (cat_id != null && cat_id != 0)
            {
                data = data.Where(x => x.cat_id == cat_id);
            }

            data = data.OrderByDescending(x => x.id);

            return View(data.ToPagedList(pageNumber, pageSize));
        }
        // GET: videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            video video = db.videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: videos/Create
        public ActionResult Create()
        {
            if (Config.getCookie("logged") == "") return RedirectToAction("Login", "Admin");
            return View();
        }

        // POST: videos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,cat_id,name,des,url,date_time,is_hot")] video video)
        {
            if (ModelState.IsValid)
            {
                db.videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(video);
        }

        // GET: videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Config.getCookie("logged") == "") return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            video video = db.videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: videos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,cat_id,name,des,url,date_time,is_hot")] video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(video);
        }

        // GET: videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Config.getCookie("logged") == "") return RedirectToAction("Login", "Admin");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            video video = db.videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            video video = db.videos.Find(id);
            db.videos.Remove(video);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
