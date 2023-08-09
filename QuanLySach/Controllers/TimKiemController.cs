using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLySach.Models;

namespace BookStore.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: TimKiem
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f, int? page)
        {
            //lay tu khoa nhap vao roi tim kiem ben trong co so du lieu.

            string tukhoa = f["txtTimKiem"].ToString();
            ViewBag.TuKhoa = tukhoa;
            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(tukhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy " + lstKQTK.Count + " sản phẩm";
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult KetQuaTimKiem(string tukhoa, int? page)
        {
            //lay tu khoa nhap vao roi tim kiem ben trong co so du lieu.
            ViewBag.TuKhoa = tukhoa;

            List<Sach> lstKQTK = db.Saches.Where(n => n.TenSach.Contains(tukhoa)).ToList();
            //phan trang
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            if (lstKQTK.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstKQTK.Count + " San pham.";
            return View(lstKQTK.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}