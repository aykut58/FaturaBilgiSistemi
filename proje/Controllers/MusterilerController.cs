using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje.Entity;

namespace proje.Controllers
{
    public class MusterilerController : Controller
    {
        DanismanlikEntities de = new DanismanlikEntities();
        // GET: Musteriler
        public ActionResult Index()
        {
            var degerler = de.musteri_tanim_table.ToList();
            return View(degerler);
        }
    }
}