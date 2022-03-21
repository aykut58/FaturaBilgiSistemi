using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proje
{
    public class FaturaDTO
    {
        public FaturaDTO(decimal Tutar, DateTime Tarih)
        {
            this.Tarih = Tarih;
            this.Tutar = Tutar;
        }
        public decimal Tutar;
        public DateTime Tarih;
    }
}