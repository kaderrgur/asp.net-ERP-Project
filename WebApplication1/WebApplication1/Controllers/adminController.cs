using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using WebApplication1.App_Class;
using System.Data.Entity;
using System.Threading;
using System.Web.Security;
using System.Globalization;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class adminController : Controller
    {

        erp_KaderGUREntities db = Context.Baglanti;

        [HttpPost]
        [Authorize(Roles = "IT,F,SP")]
        public ActionResult isortagiFatura(int IsOrtaklariId)
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            return View(Context.Baglanti.Faturalar.SqlQuery("select * from Faturalar where IsOrtaklariID=" + IsOrtaklariId).ToList());
            //return View(Context.Baglanti.spFaturaGetirIsOrtagiId(IsOrtaklariId).ToList());
        }
        public ActionResult faturalar()
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            return View(Context.Baglanti.Faturalar.ToList());
        }

        [Authorize(Roles = "IT")]
        public ActionResult kullanicilarIT()
        {
            //return View(Context.Baglanti.spKullaniciItGetir());
            return View(Context.Baglanti.Kullanici.SqlQuery("select * from Kullanici where DepartmanID=1"));
        }
        public ActionResult kullanicilarIK()
        {
            return View(Context.Baglanti.Kullanici.SqlQuery("select * from Kullanici where DepartmanID=3"));
        }
        public ActionResult kullanicilarSP()
        {
            return View(Context.Baglanti.Kullanici.SqlQuery("select * from Kullanici where DepartmanID=4"));
        }
        public ActionResult kullanicilarF()
        {
            return View(Context.Baglanti.Kullanici.SqlQuery("select * from Kullanici where DepartmanID=2"));
        }
        public ActionResult RolOnayla()
        {
            return View(Context.Baglanti.Kullanici.Where(x => x.Role == "U"));
        }
        [HttpPost]
        public ActionResult RolOnayla(int RolOnayla)
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            Kullanici onayliKullanici = Context.Baglanti.Kullanici.Find(RolOnayla);
            if (onayliKullanici.DepartmanID == 1)
            {
                onayliKullanici.Role = "IT";
            }
            else if (onayliKullanici.DepartmanID == 2)
            {
                onayliKullanici.Role = "F";
            }
            else if (onayliKullanici.DepartmanID == 3)
            {
                onayliKullanici.Role = "IK";
            }
            else if (onayliKullanici.DepartmanID == 4)
            {
                onayliKullanici.Role = "SP";
            }
            db.Entry(onayliKullanici).State = EntityState.Modified;
            Context.Baglanti.SaveChanges();
            return View(Context.Baglanti.Kullanici.Where(x => x.Role == "U"));
        }

        [Authorize(Roles = "IT,IK")]
        public ActionResult personeller()
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            db = new erp_KaderGUREntities();
            return View(Context.Baglanti.Personel.ToList());
        }
        public ActionResult personelEkle()
        {
            //ViewBag.Departman = Context.Baglanti.Departman.SqlQuery("select * from Departman where KullaniciDepartmani=0");
            ViewBag.Departman = Context.Baglanti.Departman.Where(x => x.KullaniciDepartmani == false);
            return View();
        }
        [HttpPost]
        public ActionResult personelEkle(Personel prsnl)
        {
            Context.Baglanti.Personel.Add(prsnl);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("personeller");
        }
        [HttpPost]
        public ActionResult personelSil(int personelSil)
        {
            Context.Baglanti.spPersonelSil(personelSil);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("personeller");
        }
        [HttpPost]
        public ActionResult personelGuncelle(int personelSil)
        {
            ViewBag.Departman = Context.Baglanti.Departman.SqlQuery("select * from Departman where KullaniciDepartmani=0");
            //return View(Context.Baglanti.Personel.SqlQuery("select * from Personel where PersonelId=" + personelSil));
            return View(Context.Baglanti.Personel.Find(personelSil));
        }
        [HttpPost]
        public ActionResult personelGuncelleOnay(Personel prsnl)
        {
            Personel prsnlNew = Context.Baglanti.Personel.Find(prsnl.PersonelId);
            prsnlNew.AdSoyad = prsnl.AdSoyad;
            prsnlNew.DepartmanID = prsnl.DepartmanID;
            prsnlNew.Email = prsnl.Email;
            prsnlNew.Gsm = prsnl.Gsm;
            prsnlNew.iseGirisTarihi = prsnl.iseGirisTarihi;
            prsnlNew.Maas = prsnl.Maas;
            prsnlNew.TC = prsnl.TC;

            //Context.Baglanti.spPersonelGuncelle(prsnl.PersonelId, prsnl.AdSoyad, prsnl.TC, prsnl.Email, prsnl.Gsm, prsnl.iseGirisTarihi, prsnl.DepartmanID, prsnl.Maas);
            //Context.Baglanti.Personel.SqlQuery("update Personel set AdSoyad={0},TC={1},Email={2},Gsm={3},iseGirisTarihi={4},DepartmanID={5},Maas={6} where PersonelId={7}", prsnl.AdSoyad, prsnl.TC, prsnl.Email, prsnl.Gsm, prsnl.iseGirisTarihi, prsnl.DepartmanID, prsnl.Maas, prsnl.PersonelId);
            //Personel guncellenecekpersonel = Context.Baglanti.Personel.Where(w => w.PersonelId == prsnl.PersonelId).FirstOrDefault();

            db.Entry(prsnlNew).State = EntityState.Modified;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("personeller");
        }

        [Authorize(Roles = "IT,F")]
        public ActionResult isOrtaklariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult isOrtaklariEkle(IsOrtaklari isOrtaklari)
        {
            Context.Baglanti.IsOrtaklari.Add(isOrtaklari);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("isortaklari");
        }

        [Authorize(Roles = "IT,F,SP")]
        public ActionResult isortaklari()
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            return View(Context.Baglanti.IsOrtaklari.ToList());
        }
        [HttpPost]
        public ActionResult IsOrtaklariSil(int IsOrtaklariSil)
        {
            Context.Baglanti.spIsOrtaklariSil(IsOrtaklariSil);
            Context.Baglanti.SaveChanges();
            return RedirectToAction("isortaklari");
        }
        [HttpPost]
        public ActionResult IsOrtaklariGuncelle(int IsOrtaklariGuncelle)
        {
            return View(Context.Baglanti.IsOrtaklari.Find(IsOrtaklariGuncelle));
            //return View(Context.Baglanti.IsOrtaklari.SqlQuery("select * from IsOrtaklari where IsOrtaklariId=" + IsOrtaklariGuncelle));
        }
        [HttpPost]
        public ActionResult IsOrtaklariGuncelleOnay(IsOrtaklari isOrtaklari)
        {
            IsOrtaklari ısOrtagiYeni = Context.Baglanti.IsOrtaklari.Find(isOrtaklari.IsOrtaklariId);
            ısOrtagiYeni.FirmaAd = isOrtaklari.FirmaAd;
            ısOrtagiYeni.Adres = isOrtaklari.Adres;
            ısOrtagiYeni.Email = isOrtaklari.Email;
            ısOrtagiYeni.Gsm = isOrtaklari.Gsm;

            db.Entry(ısOrtagiYeni).State = EntityState.Modified;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("isortaklari");
        }

        public ActionResult profil()
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            return View(Context.Baglanti.Kullanici.Where(x => x.Email == (HttpContext.User.Identity.Name)));
            //return View(Context.Baglanti.Kullanici.Find(HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult profilDuzenle(int kullaniciId)
        {
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);
            ViewBag.Departman = Context.Baglanti.Departman.SqlQuery("select * from Departman where KullaniciDepartmani=1");
            return View(Context.Baglanti.Kullanici.Find(kullaniciId));
            //return View(Context.Baglanti.Kullanici.SqlQuery("select * from Kullanici where KullaniciId=" + kullaniciId));
        }

        [HttpPost]
        public ActionResult profilDuzenleOnay(Kullanici kullanici)
        {
            Kullanici kullaniciYeni = Context.Baglanti.Kullanici.Find(kullanici.KullaniciId);
            kullaniciYeni.AdSoyad = kullanici.AdSoyad;
            kullaniciYeni.DogumTarihi = kullanici.DogumTarihi;
            kullaniciYeni.Email = kullanici.Email;
            kullaniciYeni.Gsm = kullanici.Gsm;
            kullaniciYeni.DepartmanID = kullanici.DepartmanID;
            kullaniciYeni.Sifre = kullanici.Sifre;
            db.Entry(kullaniciYeni).State = EntityState.Modified;
            Context.Baglanti.SaveChanges();
            return RedirectToAction("profil");
        }

        public ActionResult admin()
        {
            ViewBag.PersonelSayisi = Context.Baglanti.Personel.Count();
            ViewBag.FaturaAdedi = Context.Baglanti.Faturalar.Count();
            ViewBag.ToplamSatisFiyati = Context.Baglanti.Faturalar.Sum(x => x.ToplamFiyatKDV);
            //ViewBag.SonGelenUrun = Context.Baglanti.Stok.SqlQuery("select top 1 * from Stok Order By GelisTarih desc");
            ViewBag.SatisAdedi = Context.Baglanti.Faturalar.Sum(x => x.Miktar);
            ViewBag.OnayBekleyenAdet = Context.Baglanti.Kullanici.Count(x => x.Role == "U");
            ViewBag.OkunmamisDilekceSayisi = Context.Baglanti.Dilekce.Count(x => x.Okundu == false);

            return View(Context.Baglanti.Kullanici.Where(x => x.Email == (HttpContext.User.Identity.Name)).FirstOrDefault());
        }

        public ActionResult cikis()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "index");
        }

        [Authorize(Roles = "IT,IK")]
        public ActionResult dilekceler()
        {
            return View(Context.Baglanti.Dilekce.ToList());
        }

        //[HttpPost]
        public ActionResult DilSec(string secilenDil)
        {
            if (secilenDil != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(secilenDil);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(secilenDil);
                var cookie = new HttpCookie("Language");
                cookie.Value = secilenDil;
                Response.Cookies.Add(cookie);
            }
            return RedirectToAction("admin", "admin");
        }


    }
}