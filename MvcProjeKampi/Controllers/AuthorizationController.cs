using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{    
    public class AuthorizationController : Controller
    {
        AdminManager adminManager = new AdminManager(new EfAdminDal());
        public ActionResult Index()
        {
            var results = adminManager.GetList();
            return View(results);
        }

        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(Admin admin)
        {
            adminManager.AdminAdd(admin);
            return RedirectToAction("Index");
        }

        public ActionResult ChangeAdmin(int id)
        {
            var result = adminManager.GetByID(id);
            return View(result);
        }

        [HttpPost]
        public ActionResult ChangeAdmin(Admin admin)
        {
            adminManager.AdminUpdate(admin);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChangeAdminStatus(int id)
        {
            var result = adminManager.GetByID(id);
            if (result.Status)
            {
                result.Status = false;
            }
            else
            {
                result.Status = true;
            }
            
            adminManager.AdminUpdate(result);
            return RedirectToAction("Index");
        }

    }
}