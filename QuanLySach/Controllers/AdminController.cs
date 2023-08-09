using PagedList;
using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();
        // GET: Admin
        // Index Sach :
        public ActionResult Index(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 4;

            return View(db.Saches.ToList().OrderBy(n => n.MaSach).ToPagedList(pageNum, pageSize));
        }
        //Index DatHang :
        public ActionResult IndexDH(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 4;

            return View(db.DonHangs.ToList().OrderByDescending(n => n.MaDonHang).ToPagedList(pageNum, pageSize));
        }
        //Index ChiTietDonHang :
        public ActionResult IndexCTDH(int? page)
        {
            int pageNum = (page ?? 1);
            int pageSize = 4;

            return View(db.ChiTietDonHangs.ToList().OrderByDescending(n => n.MaDonHang).ToPagedList(pageNum, pageSize));
        }

        // Get add sach
        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MachuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            return View();
        }
        /*
        [HttpPost]
        public ActionResult ThemMoi(Sach sach,HttpPostedFileBase  fileUpload)
        {
            //luu ten cua file.

            var fileName = Path.GetFileName(fileUpload.FileName);

            //luu duong dan cua file.
            var path = Path.Combine(Server.MapPath("~/HinhAnhSP"),fileName);

            if (System.IO.File.Exists(path))
            {
               ViewBag.ThongBao = "Hình Ảnh đã tồn tại.";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            ViewBag.MachuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            return View();
        }
        */
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {


            //dua du lieu vao dropdowlist.
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");

            //kiem tra duong dan anh bia.
            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }

            //them vao co so du lieu.
            if (ModelState.IsValid)
            {
                //luu ten cua file.
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan cua file.
                var path = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                //kiem tra hinh anh da ton tai chua.
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            return View();

        }

        //Get edit sach :
        [HttpGet]
        public ActionResult ChinhSua(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach sach, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                //cập nhật.
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            return RedirectToAction("Index");
        }

        // Hien thi 
        public ActionResult HienThi(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        //Get sach :
        [HttpGet]
        public ActionResult Xoa(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        [ActionName("XoaXacNhan")]
        public ActionResult XacNhanXoa(string MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        


        [HttpGet]
        public ActionResult BaoCaoDoanhThu(int? thang, int? nam)
        {
            if (thang == null || nam == null)
            {
                // Trả về view với giá trị mặc định hoặc thông báo lỗi
                return View();
            }

            string connectionString = "Data Source=LAPTOP\\HAMIN;Initial Catalog=QuanLiBanSach;Integrated Security=True";

            string query = "SELECT MaDonHang, DaThanhToan, TinhTrangGiaoHang, NgayDat, NgayGiao, MaKH, TongTien " +
                           "FROM DonHang " +
                           "WHERE MONTH(NgayDat) = @Thang AND YEAR(NgayDat) = @Nam";

            List<DonHang> danhSachHoaDon = new List<DonHang>();
            decimal tongDoanhThu = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Thang", thang);
                command.Parameters.AddWithValue("@Nam", nam);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DonHang hoaDon = new DonHang
                    {
                        MaDonHang = reader.GetString(0),
                        DaThanhToan = reader.GetBoolean(1),
                        TinhTrangGiaoHang = reader.GetBoolean(2),
                        NgayDat = reader.GetDateTime(3),
                        NgayGiao = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                        MaKH = reader.GetString(5),
                        TongTien = reader.GetDecimal(6)
                    };

                    danhSachHoaDon.Add(hoaDon);
                    tongDoanhThu += hoaDon.TongTien;
                }
            }

            ViewBag.DanhSachHoaDon = danhSachHoaDon;
            ViewBag.TongDoanhThu = tongDoanhThu;

            return View();
        }
    }
}