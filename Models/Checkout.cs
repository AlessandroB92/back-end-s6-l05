using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l05.Models
{
    public class Checkout
    {
        public int NumeroCamera { get; set; }
        public DateTime PeriodoInizio { get; set; }
        public DateTime PeriodoFine { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataServizio { get; set; }
        public decimal TotaleImporto { get; set; }

    }
}