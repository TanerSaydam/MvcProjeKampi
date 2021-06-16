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
            var deger = mm.GetListInbox();
            return View(deger);
        }

        public ActionResult Read()
        {
            var deger = mm.GetReadList();
            return View(deger);
        }

        public ActionResult UnRead()
        {
            var deger = mm.GetUnReadList();
            return View(deger);
        }


        public ActionResult Sendbox()
        {
            var deger = mm.GetListSendbox();
            return View(deger);
        }

        public ActionResult Draft()
        {
            var deger = mm.GetListDraft();
            return View(deger);
        }

        public ActionResult Trash()
        {
            var deger = mm.GetListTrash();
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