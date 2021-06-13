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
    public class TalentController : Controller
    {
        TalentManager tm = new TalentManager(new EfTalentDal());
        
        public ActionResult Index()
        {
            var degerler = tm.GetList();

            return View(degerler);
        }     
        
        [HttpGet]
        public ActionResult AddTalent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTalent(Talent p)
        {
            p.Name = "Taner Saydam";
            p.About = "Yazılım yapmayı seviyorum";
            tm.TalentAdd(p);
            return RedirectToAction("Index");
        }

    }
}