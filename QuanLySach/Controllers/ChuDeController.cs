
using PagedList;
using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class ChuDeController : Controller
    {
        //db :
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: ChuDe
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 10;
            return View(db.ChuDes.ToList().OrderBy(n => n.MaChuDe).ToPagedList(pageNum, pageSize));
        }
        public PartialViewResult ChuDePartial()
        {
            return PartialView(db.ChuDes.Take(5).ToList());
        }
        public ViewResult SachTheoChuDe(string Ma = "")
        {
            ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaChuDe == Ma);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> sach = db.Saches.Where(n => n.MaChuDe == Ma).OrderBy(n => n.GiaBan).ToList();
            if (sach.Count == 0)
            {
                ViewBag.Sach = "Không Có Sách Nào Thuộc Chủ Đề Này";
            }
            return View(sach);
        }
        public ViewResult DanhMucChuDe()
        {
            return View(db.ChuDes.ToList());
        }

        //CRUD chu de :
        //Add chu de 
        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(ChuDe chude)
        {
            if (ModelState.IsValid)
            {
                db.ChuDes.Add(chude);
                db.SaveChanges();
            }
            return View();
        }

    }
}