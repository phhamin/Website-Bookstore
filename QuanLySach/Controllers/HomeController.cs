using PagedList;
using PagedList.Mvc;
using QuanLySach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        QuanLiBanSachEntities db = new QuanLiBanSachEntities();

        // GET: Home    
        public ActionResult Index(int? page)
        {
            //tao bien so san pham tren 1 trang,  
            int pageSize = 8;
            //taobien so trang.
            var pageNumber = (page ?? 1);
            return View(db.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }


    }
}