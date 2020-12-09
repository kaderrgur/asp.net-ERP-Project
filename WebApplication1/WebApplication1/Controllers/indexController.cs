using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.App_Class;
using System.Web.Security;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class indexController : Controller
    {
        erp_KaderGUREntities db = new erp_KaderGUREntities();

        public ActionResult index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult index(Kullanici kullanici)
        {
            var k = db.Kullanici.FirstOrDefault(x => x.Email == kullanici.Email && x.Sifre == kullanici.Sifre);
            if (k != null)
            {
                FormsAuthentication.SetAuthCookie(k.Email, false);
                //ViewBag.Mesaj = k.AdSoyad.ToString();
                return RedirectToAction("admin", "admin");
            }
            else
            {
                ViewBag.Mesaj = "Hatalı e-mail adresi veya şifre girdiniz.";
                return View();
            }

        }

        public ActionResult uyeol()
        {
            ViewBag.Departman = Context.Baglanti.Departman.SqlQuery("select * from Departman where KullaniciDepartmani = 1").ToList();
            return View();
        }

        [HttpPost]
        public ActionResult kullaniciEkle(Kullanici kllnici)
        {
            kllnici.Role = "U";
            Context.Baglanti.Kullanici.Add(kllnici);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("index");
        }
        /*
        [HttpPost]
        public ActionResult index(string TC,string Sifre)
        {
            return View();
        }*/
    }
}