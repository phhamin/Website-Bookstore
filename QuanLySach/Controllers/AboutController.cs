using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            return View();
        }
        // Gioi Thieu
        public ActionResult GioiThieu()
        {
            return View();
        }
        //Lien He
        public ActionResult LienHe()
        {
            return View();
        }
        // Tin tuc :
        public ActionResult TinTuc()
        {
            return View();
        }

    }
}