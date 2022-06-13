using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KiemTra_PhanMinhTri1.Models;

namespace KiemTra_PhanMinhTri1.Controllers
{
    public class HomeController : Controller
    {
        DataDataContext data = new DataDataContext();
        public ActionResult ListHP()
        {
            var all_hp = from ss in data.HocPhans select ss;
            return View(all_hp);
        }
    }
}