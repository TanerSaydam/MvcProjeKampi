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
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        HeadingValidator HeadingValidator = new HeadingValidator();
        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading()
        {

            //int id = Convert.ToInt32(Session["WriterID"]);
            int id = 4;
            List<Heading> deger = hm.GetListById(id).ToList();
            return View(deger);
        }

        [HttpGet]
        public ActionResult AddHeading()
        {
            //int id = Convert.ToInt32(Session["WriterID"]);
            int id = 4;
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
        public ActionResult EditHeading(int id)
        {
            //int kid = Convert.ToInt32(Session["WriterID"]);
            int kid = 4;
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



    }
}