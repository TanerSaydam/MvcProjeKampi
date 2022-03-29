using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class WriterController : Controller
    {
        // GET: Writer
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writerValidator = new WriterValidator();
        public ActionResult Index()
        {
            var writervalue = wm.GetList();
            return View(writervalue);
        }

        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWriter(Writer p, HttpPostedFileBase file)
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
                wm.WriterAdd(p);
                return RedirectToAction("Index");
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
       
        public ActionResult DeleteWriter(int id)
        {
            var writervalue = wm.GetById(id);            
            wm.WriterDelete(writervalue);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writervalue = wm.GetById(id);
            return View(writervalue);
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
                return RedirectToAction("Index");
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