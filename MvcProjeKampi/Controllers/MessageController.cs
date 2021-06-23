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
    [Authorize]
    public class MessageController : Controller
    {
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageValidator messageValidator = new MessageValidator();
        public ActionResult Inbox()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListInbox(p);
            return View(deger);
        }

        public ActionResult Read()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetReadList(p);
            return View(deger);
        }

        public ActionResult UnRead()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetUnReadList(p);
            return View(deger);
        }


        public ActionResult Sendbox()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListSendbox(p);
            return View(deger);
        }

        public ActionResult Draft()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListDraft(p);
            return View(deger);
        }

        public ActionResult Trash()
        {
            string p = (string)Session["WriterMail"];
            var deger = mm.GetListTrash(p);
            return View(deger);
        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult NewMessage(Message p, string MessageContent)
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

        public ActionResult GetInBoxMessageDetails(int id)
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
        }      
    }
}