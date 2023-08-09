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
    public class SachController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: Sach
        public ActionResult Index()
        {
            return View();
        }
        //List danh sach :
        public ActionResult DanhsachSach()
        {
            return View(db.Saches.ToList());
        }

      
        public ViewResult XemChiTiet(String Ma = "")
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
    }
}

