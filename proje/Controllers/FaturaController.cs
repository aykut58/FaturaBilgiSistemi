using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proje.Entity;

namespace proje.Controllers
{
    public class FaturaController : Controller
    {
        DanismanlikEntities de = new DanismanlikEntities();
        // GET: Fatura
        public ActionResult Index()
        {
            int musteriId = Convert.ToInt32(Request.QueryString["musteriId"]); 
            List<FaturaDTO> faturaDTOs = new List<FaturaDTO>();
            var Faturalar = de.musteri_fatura_table.Where(m => m.MUSTERI_ID == musteriId).ToList();
            foreach (var fatura in Faturalar)
            {
                faturaDTOs.Add(new FaturaDTO(fatura.FATURA_TUTARI.Value*-1,fatura.FATURA_TARIHI.Value));
                if(fatura.ODEME_TARIHI.HasValue) // fatura ödenmişse
                faturaDTOs.Add(new FaturaDTO(fatura.FATURA_TUTARI.Value, fatura.ODEME_TARIHI.Value));
            }
            faturaDTOs = faturaDTOs.OrderBy(fatura => fatura.Tarih).ToList();
            // her gün için toplam borç tutar hesabı
            List<FaturaDTO> faturaDTOs2 = new List<FaturaDTO>();
            decimal toplam = 0;
            DateTime SonGun=faturaDTOs[0].Tarih;
            foreach (var fatura in faturaDTOs)
            {
                if (SonGun.Equals(fatura.Tarih))
                    toplam += fatura.Tutar;
                else
                {
                    faturaDTOs2.Add(new FaturaDTO(toplam, SonGun));
                    toplam = fatura.Tutar;
                    SonGun = fatura.Tarih;
                }

            }
            // en yüksek borcu bulma
            decimal ToplamBorc=0;
            decimal MaxBorc = 0;
            DateTime MaxBorcGun=faturaDTOs2[0].Tarih;
            foreach (var fatura in faturaDTOs2)
            {
                ToplamBorc += -1 * fatura.Tutar;
                if(MaxBorc<ToplamBorc)
                {
                    MaxBorcGun = fatura.Tarih;
                    MaxBorc = ToplamBorc;
                }
            }

            ViewData["ToplamBorc"] = MaxBorc;
            ViewData["Tarih"] = MaxBorcGun;
            return View();
            
        }
    }
}