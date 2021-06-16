using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public PartialViewResult GelenKutuSolMenu()
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

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetByID(id);
            return View(contactvalues);
        }
    }
}