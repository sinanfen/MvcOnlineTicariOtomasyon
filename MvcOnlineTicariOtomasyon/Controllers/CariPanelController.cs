using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajs.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailId = c.Caris.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            ViewBag.mailId = mailId;
            var toplamSatis = c.SatisHarekets.Where(x => x.Cariid == mailId).Count();
            ViewBag.toplamSatis = toplamSatis;
            var toplamTutar = c.SatisHarekets.Where(x => x.Cariid == mailId).Sum(y => y.ToplamTutar);
            ViewBag.toplamTutar = toplamTutar;
            var toplamUrun = c.SatisHarekets.Where(x => x.Cariid == mailId).Sum(y => y.Adet);
            ViewBag.toplamUrun = toplamUrun;
            var adSoyad = c.Caris.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adSoyad = adSoyad;
            var cariSehir = c.Caris.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.cariSehir = cariSehir;
            return View(degerler);
        }
        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajs.Where(x => x.Alici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenSayisi = c.Mesajs.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajs.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            ViewBag.gidenSayisi = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajs.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelenSayisi = c.Mesajs.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajs.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            ViewBag.gidenSayisi = gidenSayisi;
            return View(mesajlar);
        }
        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajs.Where(x => x.MesajID == id).ToList();
            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajs.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajs.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            ViewBag.gidenSayisi = gidenSayisi;
            return View(degerler);
        }

        [HttpGet]
        [Authorize]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajs.Count(x => x.Alici == mail).ToString();
            var gidenSayisi = c.Mesajs.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            ViewBag.gidenSayisi = gidenSayisi;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesaj m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail;
            c.Mesajs.Add(m);
            c.SaveChanges();
            return View();
        }
        [Authorize]
        public ActionResult KargoTakip(string p)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(p));
            return View(k.ToList());
        }
        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(degerler);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index","Login");
        }

        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Caris.Where(x => x.CariMail == mail).Select(y=>y.CariId).FirstOrDefault();
            var cariBul = c.Caris.Find(id);
            return PartialView("Partial1",cariBul);
        }

        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajs.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }

        public ActionResult CariBilgiGuncelle(Cari cr)
        {
            var cari = c.Caris.Find(cr.CariId);
            cari.CariAd = cr.CariAd;
            cari.CariSoyad = cr.CariSoyad;
            cari.CariSifre = cr.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}