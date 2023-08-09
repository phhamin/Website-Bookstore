using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using QuanLySach.Models;

namespace BookStore.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: NguoiDung
        string LayMaKH()
        {
            var maMax = db.KhachHangs.ToList().Select(n => n.MaKH).Max();
            if (maMax == null)
                return "KH001";
            string so = maMax.Substring(2);

            // Chuyển đổi số thành giá trị số nguyên
            int soHienTai = int.Parse(so);

            // Tăng giá trị số hiện tại lên 1
            int soMoi = soHienTai + 1;

            // Tạo mã HD mới bằng cách kết hợp "KH" với số mới đã định dạng
            string maKH = "KH" + soMoi.ToString("D3");

            return maKH;
        }
        public ActionResult Index()
        {
            return View();
        }
        // Get DangKi 
        public ActionResult DangKi()
        {
            return View();
        }

        //Get thong tin nguoi dung 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKi(KhachHang kh)
        {//neu hop le thy cho them vao
            if (ModelState.IsValid)
            {
                kh.MaKH = LayMaKH();
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(kh);
        }

        // Get Dang Nhap
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string tk = f["txtTaiKhoan"].ToString();
            string mk = f["txtMatKhau"].ToString();
            if (tk == "" || mk == "")
            {
                ViewBag.ThongBao = "BẠN CHƯA NHẬP TÀI KHOẢN HOẶC MẬT KHẨU";
                return View();
            }
            Session["ID"] = null;
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == tk && n.MatKhau == mk);
            tbl_Login admin = db.tbl_Login.SingleOrDefault(n => n.Username == tk && n.Password == mk);
            
            if (kh != null)
            {
                Session["TaiKhoan"] = kh;
                Session["ID"] = kh.MaKH;
                return RedirectToAction("Index", "Home");
            }
            else if (admin != null)
            {
                Session["TaiKhoanAdmin"] = admin;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.ThongBao = "TÊN TÀI KHOẢN HOẶC MẬT KHẨU CỦA BẠN KHÔNG ĐÚNG";
            return View();
        }

        // GET: NguoiDung/DangXuat
        public ActionResult DangXuat()
        {
            // Xóa phiên đăng nhập của người dùng
            Session["TaiKhoan"] = null;
            Session["TaiKhoanAdmin"] = null;

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("../Home/Index");
        }

    }
}