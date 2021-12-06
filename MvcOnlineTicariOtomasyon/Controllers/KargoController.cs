using MvcOnlineTicariOtomasyon.Models.Siniflar;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo
        Context c = new Context();
        public ActionResult Index()
        {
            var kargolar = c.KargoDetays.ToList();
            return View(kargolar);
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            int sayi1, sayi2, sayi3, sayi4, sayi5;
            int harf, harf2, harf3, harf4;
            sayi1 = rnd.Next(1, 9);
            sayi2 = rnd.Next(0, 9);
            sayi3 = rnd.Next(0, 9);
            sayi4 = rnd.Next(1, 9);
            harf = rnd.Next(65, 91);
            sayi5 = rnd.Next(65, 91);
            harf2 = rnd.Next(65, 91);
            harf3 = rnd.Next(65, 91);
            harf4 = rnd.Next(65, 91);
            if (harf == harf2)
            {
                harf2 = rnd.Next(65, 91);
            }
            if (harf2 == harf3)
            {
                harf3 = rnd.Next(65, 91);
            }
            if (harf3 == harf4)
            {
                harf4 = rnd.Next(65, 91);
            }
            if (sayi1 == sayi2)
            {
                sayi2 = rnd.Next(1, 9);
            }
            if (sayi2 == sayi3)
            {
                sayi3 = rnd.Next(0, 9);
            }
            if (sayi3 == sayi4)
            {
                sayi4 = rnd.Next(1, 9);
            }
            if (sayi4 == sayi5)
            {
                sayi5 = rnd.Next(0, 9);
            }
            char karakter;
            char karakter2;
            char karakter3;
            char karakter4;
            karakter = Convert.ToChar(harf);
            karakter2 = Convert.ToChar(harf2);
            karakter3 = Convert.ToChar(harf3);
            karakter4 = Convert.ToChar(harf4);
            string kod = sayi1.ToString() + sayi2.ToString() + karakter + sayi3.ToString() + sayi4.ToString() + karakter2 + sayi4.ToString() + karakter3 + sayi5.ToString() + karakter4;
            ViewBag.takipKod = kod;
            return View();
        }

        [HttpPost]
        public ActionResult YeniKargo(KargoDetay d)
        {
            c.KargoDetays.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index", "Kargo");
        }

        public ActionResult KargoTakip(string id)
        {
            var degerler = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();
           
            return View(degerler);
        }


        public ActionResult QR(int id)
        {
            var deger = c.KargoDetays.Find(id);
            ViewBag.dgr = deger.TakipKodu;
            return View();
        }

        [HttpPost]
        public ActionResult QR(string kod)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator kodUret = new QRCodeGenerator();
                QRCodeGenerator.QRCode kareKod = kodUret.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap resim = kareKod.GetGraphic(10))
                {
                    resim.Save(ms, ImageFormat.Png);
                    ViewBag.karekodimage = "data:image/png;base64," + Convert.ToBase64String
                        (ms.ToArray());
                }
            }
            return View();
        }
    }
}