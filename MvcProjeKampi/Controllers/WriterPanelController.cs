using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace MvcProjeKampi.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        HeadingValidator HeadingValidator = new HeadingValidator();
        Context c = new Context();
        public ActionResult WriterProfile()
        {
            string p = (string)Session["WriterMail"];
            var deger1 = c.Writers.Where(x => x.WriterMail == p).Select(a => a.WriterID).FirstOrDefault();
            int id = deger1;
            var deger = wm.GetById(id);
            return View(deger);
        }

        [HttpPost]
        public ActionResult EditWriter(Writer p, HttpPostedFileBase file)
        {
            if (file != null)
            {
                var dosya_formati_adi = Path.GetFileName(file.FileName);
                string dosya_adi = Guid.NewGuid().ToString() + dosya_formati_adi;
                p.WriterImage = dosya_adi;                
                file.SaveAs(Server.MapPath("/AdminLTE-3.0.4/dist/img/" + dosya_adi));                
            }

            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("AllHeading");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult MyHeading(string p)
        {

            Context c = new Context();
            p = (string)Session["WriterMail"];
            var deger1 = c.Writers.Where(x => x.WriterMail == p).Select(a => a.WriterID).FirstOrDefault();
            int id = deger1;
            List<Heading> deger = hm.GetListById(id).ToList();
            return View(deger);
        }

        [HttpGet]
        public ActionResult AddHeading(string p)
        {
            Context c = new Context();
            p = (string)Session["WriterMail"];
            var deger = c.Writers.Where(x => x.WriterMail == p).Select(a => a.WriterID).FirstOrDefault();
            int id = deger;
            ViewBag.wrt = id;
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();            
            ViewBag.vlc = valuecategory;
            return View();
        }

        [HttpPost]
        public ActionResult AddHeading(Heading p)
        {
            ValidationResult results = HeadingValidator.Validate(p);
            if (results.IsValid)
            {
                p.HeadingStatus = true;
                p.HeadingDate = DateTime.Now;
                hm.HeadingAdd(p);
                return RedirectToAction("MyHeading");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult DeleteHeading(int id)
        {
            var headingvalues = hm.GetByID(id);
            if (headingvalues.HeadingStatus == true)
            {
                headingvalues.HeadingStatus = false;
                hm.HeadingDelete(headingvalues);
            }
            else
            {
                headingvalues.HeadingStatus = true;
                hm.HeadingDelete(headingvalues);
            }
            return RedirectToAction("MyHeading");
        }

        [HttpGet]
        public ActionResult EditHeading(int id, string p)
        {
            Context c = new Context();
            p = (string)Session["WriterMail"];
            var deger = c.Writers.Where(x => x.WriterMail == p).Select(a => a.WriterID).FirstOrDefault();
            int kid = deger;            
            ViewBag.wrt = kid;
            var headingvalues = hm.GetByID(kid);
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();            
            ViewBag.vlc = valuecategory;
            return View(headingvalues);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            ValidationResult results = HeadingValidator.Validate(p);
            if (results.IsValid)
            {
                hm.HeadingUpdate(p);
                return RedirectToAction("MyHeading");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        public ActionResult AllHeading(int p = 1)
        {
            var deger = hm.GetList().ToPagedList(p,4);
            return View(deger);
        }

        
    }
}