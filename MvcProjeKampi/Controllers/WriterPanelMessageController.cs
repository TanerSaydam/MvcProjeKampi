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
    public class WriterPanelMessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager cm = new ContactManager(new EfContactDal());
        WriterManager vm = new WriterManager(new EfWriterDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox() //Bu Tamam
        {
            var deger = mm.GetListInbox();
            return View(deger);
        }

        public ActionResult Read() //Tamam
        {
            var deger = mm.GetReadList();
            return View(deger);
        }

        public ActionResult UnRead() //Tamam
        {
            var deger = mm.GetUnReadList();
            return View(deger);
        }


        public ActionResult Sendbox() //Tamam
        {
            var deger = mm.GetListSendbox();
            return View(deger);
        }

        public ActionResult Draft() //Tamam
        {
            var deger = mm.GetListDraft();
            return View(deger);
        }

        public ActionResult Trash() //Tamam
        {
            var deger = mm.GetListTrash();
            return View(deger);
        }

        public PartialViewResult GelenKutuSolMenu() //Tamam
        {
            var contactvalues = cm.GetList();
            ViewBag.sayi = contactvalues.Count();

            var deger1 = mm.GetListInbox();
            ViewBag.sayi1 = deger1.Count();

            var deger2 = mm.GetListSendbox();
            ViewBag.sayi2 = deger2.Count();

            var deger3 = mm.GetListDraft();
            ViewBag.sayi3 = deger3.Count();

            var deger4 = mm.GetListTrash();
            ViewBag.sayi4 = deger4.Count();

            var deger5 = mm.GetReadList();
            ViewBag.sayi5 = deger5.Count();

            var deger6 = mm.GetUnReadList();
            ViewBag.sayi6 = deger6.Count();
            return PartialView();

        }

        [HttpGet]
        public ActionResult NewMessage() //Tamam
        {
            int id = 4;
            var deger = vm.GetById(id);
            ViewBag.mail = deger.WriterMail;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p, string MessageContent) //Tamam
        {
            ValidationResult results = messageValidator.Validate(p);
            if (results.IsValid)
            {
                p.MessageDate = DateTime.Now;
                //p.MessageContent = MessageContent;
                p.MessageStatus = "Gönderilen";
                mm.MessageAdd(p);
                return RedirectToAction("Sendbox");
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

        public ActionResult GetInBoxMessageDetails(int id) //Tamam
        {
            var values = mm.GetByID(id);
            values.MessageRead = true;
            mm.MessageUpdate(values);
            return View(values);
        }

        public ActionResult GetSendBoxMessageDetails(int id)
        {
            var values = mm.GetByID(id);
            values.MessageRead = true;
            mm.MessageUpdate(values);
            return View(values);
        } //Tamam
    }
}