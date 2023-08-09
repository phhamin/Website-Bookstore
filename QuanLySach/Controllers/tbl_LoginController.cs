using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySach.Models;

namespace QuanLySach.Controllers
{
    public class tbl_LoginController : Controller
    {
        private QuanLiBanSachEntities db = new QuanLiBanSachEntities();

        // GET: tbl_Login
        public ActionResult Index()
        {
            return View(db.tbl_Login.ToList());
        }

        // GET: tbl_Login/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Login tbl_Login = db.tbl_Login.Find(id);
            if (tbl_Login == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Login);
        }

        // GET: tbl_Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tbl_Login/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password")] tbl_Login tbl_Login)
        {
            if (ModelState.IsValid)
            {
                db.tbl_Login.Add(tbl_Login);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tbl_Login);
        }

        // GET: tbl_Login/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Login tbl_Login = db.tbl_Login.Find(id);
            if (tbl_Login == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Login);
        }

        // POST: tbl_Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password")] tbl_Login tbl_Login)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Login).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Login);
        }

        // GET: tbl_Login/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Login tbl_Login = db.tbl_Login.Find(id);
            if (tbl_Login == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Login);
        }

        // POST: tbl_Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Login tbl_Login = db.tbl_Login.Find(id);
            db.tbl_Login.Remove(tbl_Login);
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
