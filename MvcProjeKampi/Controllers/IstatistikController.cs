using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class IstatistikController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        WriterManager wm = new WriterManager(new EfWriterDal());


        // GET: Istatistik
        public ActionResult Index()
        {
            ViewBag.Deger1 = cm.GetList().Count(); //Kategori Sayısı
            ViewBag.Deger2 = hm.GetList().Where(x=> x.CategoryID == 20).Count(); //Yazılım kategorisindeki başlık sayısı
            ViewBag.Deger3 = wm.GetList().Where(x => x.WriterName.Contains("a") || x.WriterName.Contains("A")).Count(); //içinde harfi geçen yazar sayısı
            ViewBag.Deger4 = cm.GetList().Where(x=> x.CategoryID == (hm.GetList().GroupBy(h=> h.CategoryID).OrderByDescending(z=> z.Count()).Select(y=> y.Key).FirstOrDefault())).Select(k=> k.CategoryName).FirstOrDefault(); //En fazla başlığa sahip kategori
            ViewBag.Deger5 = cm.GetList().Where(x=> x.CategoryStatus == true).Count(); //True olan kategori sayısı

            return View();
        }
    }
}