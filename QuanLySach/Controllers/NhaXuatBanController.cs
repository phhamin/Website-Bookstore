using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class NhaXuatBanController : Controller
    {
        //Db :
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: NhaXuatBan
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList());
        }
        public ViewResult SachTheoNhaXuatBan(string Ma = "")
        {
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == Ma);
            if (nxb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> sach = db.Saches.Where(n => n.MaNXB == Ma).OrderBy(n => n.GiaBan).ToList();
            if (sach.Count == 0)
            {
                ViewBag.Sach = "Không có Cuốn Sách Nào thuộc Nhà Xuất Bản Này!.";
                return null;
            }
            return View(sach);

        }
        public ViewResult DanhMucNhaXuatBan()
        {
            return View(db.NhaXuatBans.ToList());
        }
    }
}