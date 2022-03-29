using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace MvcProjeKampi.Controllers
{
    [AllowAnonymous]    
    public class LoginController : Controller
    {
        AdminManager an = new AdminManager(new EfAdminDal());
        WriterLoginManager wm = new WriterLoginManager(new EfWriterDal());

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string username, string password, Admin p)
        {
            //var crypto = new SimpleCrypto.PBKDF2();

            var degerList = an.GetList();
            var deger = degerList.Where(x => x.AdminUserName == username && x.AdminPassword == password).FirstOrDefault();


            if (deger != null)
            {
                FormsAuthentication.SetAuthCookie(deger.AdminUserName, false);
                Session["AdminUserName"] = deger.AdminUserName;
                return RedirectToAction("Index", "AdminCategory");
                
                //if (deger.AdminPassword == crypto.Compute(password,deger.Salt))
                //{
                //    FormsAuthentication.SetAuthCookie(deger.AdminUserName, false);
                //    Session["AdminUserName"] = deger.AdminUserName;
                //    return RedirectToAction("Index", "AdminCategory");
                //}                
            }

            ViewBag.Hata = "Kullanıcı adı ya da şifre yanlış";
            return View();
        }       

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Headings", "Default");
        }        

        [HttpGet]
        public ActionResult WriterLogin()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult WriterLogin(Writer p)
        {
            //var response = Request["g-recaptcha-response"];
            //const string secret = "6LcotuYSAAAAAHmLlVOMatQlYyOqqeq08sTlGWoE";
            //var client = new WebClient();

            //var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secret, response));
            //var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);
            //if (!captchaResponse.Success)
            //{
            //    TempData["Message"] = "Lütfen güvenliği doğrulayınız.";
            //    return View();
            //}

            Context c = new Context();
            //var crypto = new SimpleCrypto.PBKDF2();

            //var deger = c.Writers.FirstOrDefault(x => x.WriterMail == p.WriterMail && x.WriterPassword == p.WriterPassword);
            var deger = wm.GetWriter(p.WriterMail, p.WriterPassword);
            if (deger != null)
            {
                FormsAuthentication.SetAuthCookie(deger.WriterMail, false);
                Session["WriterMail"] = deger.WriterMail;
                return RedirectToAction("MyContent", "WriterPanelContent");
            }

            ViewBag.Hata = "Kullanıcı adı ya da şifre yanlış";
            return View();
        }

        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }     
    }
}