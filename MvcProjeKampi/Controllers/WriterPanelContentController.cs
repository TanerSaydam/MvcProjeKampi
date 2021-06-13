using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelContentController : Controller
    {
        // GET: Content
        ContentManager cn = new ContentManager(new EfContentDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyContent()
        {
            int id = 4;
            var contentvalues = cn.GetListByWriter(id);
            return View(contentvalues);
        }

        [HttpGet]
        public ActionResult EditContent(int id)
        {
            var deger = cn.GetByID(id);
            return View(deger);
        }

        [HttpPost]
        public ActionResult EditContent(Content p)
        {
            cn.ContentUpdate(p);
            return RedirectToAction("MyContent");
        }
    }
}