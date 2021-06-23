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
        Content c = new Content();
        public ActionResult Inbox() //Bu Tamam
        {
            string p = (string)Session["WriterMail"];            
            var deger = mm.GetListInbox(p);
            return View(deger);
        }

        public ActionResult Read() //Tamam
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetReadList(p);
            return View(deger);
        }

        public ActionResult UnRead() //Tamam
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetUnReadList(p);
            return View(deger);
        }


        public ActionResult Sendbox() //Tamam
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListSendbox(p);
            return View(deger);
        }

        public ActionResult Draft() //Tamam
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListDraft(p);
            return View(deger);
        }

        public ActionResult Trash() //Tamam
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListTrash(p);
            return View(deger);
        }

        public PartialViewResult GelenKutuSolMenu() //Tamam
        {
            var contactvalues = cm.GetList();
            ViewBag.sayi = contactvalues.Count();

            string p = (string)Session["WriterMail"];

            var deger1 = mm.GetListInbox(p);
            ViewBag.sayi1 = deger1.Count();

            var deger2 = mm.GetListSendbox(p);
            ViewBag.sayi2 = deger2.Count();

            var deger3 = mm.GetListDraft(p);
            ViewBag.sayi3 = deger3.Count();

            var deger4 = mm.GetListTrash(p);
            ViewBag.sayi4 = deger4.Count();

            var deger5 = mm.GetReadList(p);
            ViewBag.sayi5 = deger5.Count();

            var deger6 = mm.GetUnReadList(p);
            ViewBag.sayi6 = deger6.Count();
            return PartialView();

        }

        [HttpGet]
        public ActionResult NewMessage() //Tamam
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p, string MessageContent) //Tamam
        {
            string send = (string)Session["WriterMail"];
            ValidationResult results = messageValidator.Validate(p);
            if (results.IsValid)
            {
                p.SenderMail = send;
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