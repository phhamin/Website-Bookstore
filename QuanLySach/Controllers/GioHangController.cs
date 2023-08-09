using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class GioHangController : Controller
    {
        static Random r = new Random();
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }

        // List chua cac hang 
        public List<GioHang> LayGioHang()
        {
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang == null)
            {
                //tao mot gio hang moi
                lstGioHang = new List<GioHang>();
                //gan lai gio hang.
                Session["GioHang"] = lstGioHang;

            }
            //xuat ra gio hang.
            return lstGioHang;
        }

        // Add gio hang 
        public ActionResult ThemGioHang(string Ma, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay ra gio hang.
            List<GioHang> lstGioHang = LayGioHang();
            //tim sach trong gio hang.
            GioHang gh = lstGioHang.Find(n => n.iMaSP == Ma);
            if (gh == null)
            {
                gh = new GioHang(Ma);
                //add san pham moi them vao list.
                lstGioHang.Add(gh);
                return Redirect(strURL);
            }
            else
            {
                //Tang gio hang len ++1
                gh.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cat nhap gio hang
        public ActionResult CapNhatGioHang(string Ma, FormCollection f)
        {
            // tao mot ma san pham theo ma sach.
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            //xem thu ma sach do co chua.
            if (sach == null)
            {
                //neu khong co thy thong bao loi.
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang ra.
            List<GioHang> lstGioHang = LayGioHang();
            //xem thu ma san pham co chua.
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == Ma);
            //neu ton tai thy cho chih sua.
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        //Delete gio hang 
        public ActionResult XoaGioHang(string Ma)
        {
            // tao mot ma san pham theo ma sach.
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == Ma);
            //xem thu ma sach do co chua.
            if (sach == null)
            {
                //neu khong co thy thong bao loi.
                Response.StatusCode = 404;
                return null;
            }
            //lay gio hang ra.
            List<GioHang> lstGioHang = LayGioHang();
            //xem thu ma san pham co chua.
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSP == Ma);
            //neu ton tai thy cho chih sua.
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSP == Ma);

            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("GioHang");
        }

        // Return vè Home <Trang Trủ >
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        // Sum so luong 
        public int TongSoLuong()
        {
            int iTongSoLuong = 0;
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }

        // Tong tien :
        public double TongTien()
        {
            double iTongTien = 0;
            //Tao list gio hang de luu lai gio hang.
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            //neu khong co gio hang
            if (lstGioHang != null)
            {
                iTongTien = lstGioHang.Sum(n => n.iThanhTien);
            }
            return iTongTien;
        }
        // Hien thi khi count = 0 
        public PartialViewResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        // Edit gio hang
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");

            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);

        }
        string LayMaDH()
        {
            var maMax = db.DonHangs.ToList().Select(n => n.MaDonHang).Max();
            if (maMax == null)
                return "DH001";
            string so = maMax.Substring(2);

            // Chuyển đổi số thành giá trị số nguyên
            int soHienTai = int.Parse(so);

            // Tăng giá trị số hiện tại lên 1
            int soMoi = soHienTai + 1;

            // Tạo mã HD mới bằng cách kết hợp "KH" với số mới đã định dạng
            string maDH = "DH" + soMoi.ToString("D3");

            return maDH;
        }
        // Dat hang cho khach
        public ActionResult DatHang()
        {
            //xem thu da dang nhap chua.
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"] == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            //xem thu trong gio hang co gio hang nao chua.
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //them don hang.
            //tao ra don hang
            DonHang dh = new DonHang();
            //khach hang mua don hang la ai.
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            //lay don hang trong gio hang.
            List<GioHang> gh = LayGioHang();
            // Ma đơn hàng :

            dh.MaDonHang = r.Next().ToString();
            //tao don hang voi ma khach hang.
            dh.MaKH = kh.MaKH;
            //ngay mua hang.
            dh.NgayDat = DateTime.Now;
            //tong tien 
            decimal tongTien = 0;

            // Them chi tiet don hang
            foreach (var item in gh)
            {
                // Tạo chi tiet don hang
                ChiTietDonHang ctdh = new ChiTietDonHang();
                // ...
                decimal thanhTien = 0;
                if (ctdh.DonGia != null && ctdh.SoLuong != null)
                {
                    thanhTien = (decimal)ctdh.DonGia * (decimal)ctdh.SoLuong;
                }

                // Thêm vào tổng tiền đơn hàng
                tongTien += thanhTien;

                // ...
            }

            // Lưu tổng tiền vào đơn hàng
            dh.TongTien = tongTien;

            //them don hang vao csdl.
            dh.MaDonHang = LayMaDH();
            db.DonHangs.Add(dh);

            db.SaveChanges();
            //them chi tiet don hang.
            foreach (var item in gh)
            {
                //tao chi tiet don hang.
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = dh.MaDonHang;
                ctdh.MaSach = item.iMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.iDonGia;

                db.ChiTietDonHangs.Add(ctdh);
            }
            db.SaveChanges();
            ViewBag.tile = "Bạn đã đặt hàng thành công";
            return RedirectToAction("Index", "Home");
        }

    }
}
