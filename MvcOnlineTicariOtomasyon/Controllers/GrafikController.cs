using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            var grafikCiz = new Chart(600, 600);
            grafikCiz.AddTitle("Kategori ~ Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Bilgisayar" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikCiz.ToWebImage().GetBytes(), "image/jpeg");
        }

        Context c = new Context();
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(width: 1000, height: 1000).AddTitle("Stoklar").AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }

        public ActionResult Index4()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(UrunListesi(), JsonRequestBehavior.AllowGet);
        }

        public List<Sinif1> UrunListesi()
        {
            List<Sinif1> sinif = new List<Sinif1>();
            sinif.Add(new Sinif1()
            {
                urunAd = "Bilgisayar",
                Stok = 60
            });
            sinif.Add(new Sinif1()
            {
                urunAd = "Beyaz Eşya",
                Stok = 30
            });
            sinif.Add(new Sinif1()
            {
                urunAd = "Küçük Ev Aletleri",
                Stok = 200
            });
            sinif.Add(new Sinif1()
            {
                urunAd = "Monitör",
                Stok = 70
            });
            sinif.Add(new Sinif1()
            {
                urunAd = "Telefon",
                Stok = 90
            });
            return sinif;
        }

        public ActionResult Index5()
        {
            return View();
        }

        public ActionResult VisualizeUrunResult2()
        {
            return Json(UrunListesi2(), JsonRequestBehavior.AllowGet);
        }

        public List<Sinif2> UrunListesi2()
        {
            List<Sinif2> snf = new List<Sinif2>();
            using (var context=new Context())
            {
                snf = c.Uruns.Select(x => new Sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok
                }).ToList();
            }

            return snf;
        }

        public ActionResult Index6()
        {
            return View();
        }

        public ActionResult Index7()
        {
            return View();
        }
    }
}