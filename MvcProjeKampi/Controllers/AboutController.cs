using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class AboutController : Controller
    {
        AboutManager abm = new AboutManager(new EfAboutDal());
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }

        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            p.AboutStatus = true;
            abm.AboutAdd(p);
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {
            return PartialView();
        }

        public ActionResult DurumDegistir(int id)
        {
            var deger = abm.GetByID(id);
            if (deger.AboutStatus == true)
            {
                deger.AboutStatus = false;
                abm.AboutUpdate(deger);
            }
            else
            {
                deger.AboutStatus = true;
                abm.AboutUpdate(deger);
            }
            return RedirectToAction("Index");
        }

    }
}