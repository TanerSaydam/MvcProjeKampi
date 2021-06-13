using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]    
    public class LoginController : Controller
    {
        AdminManager an = new AdminManager(new EfAdminDal());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, Admin p)
        {            
            var crypto = new SimpleCrypto.PBKDF2();

            var deger = an.GetList().Where(x => x.AdminUserName == username).FirstOrDefault();

            if (deger != null)
            {
                if (deger.AdminPassword == crypto.Compute(password,deger.Salt))
                {
                    FormsAuthentication.SetAuthCookie(deger.AdminUserName, false);
                    Session["AdminUserName"] = deger.AdminUserName;
                    return RedirectToAction("Index", "AdminCategory");
                }                
            }

            ViewBag.Hata = "Kullanıcı adı ya da şifre yanlış";
            return View();
        }       

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }        
    }
}