using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace back_end_s6_l05.Models
{
    public class ServizioAggiuntivo
    {
        public int ID { get; set; }
        public int PrenotazioneID { get; set; }
        public string Descrizione { get; set; }
        public int Quantita { get; set; }
        public decimal Prezzo { get; set; }
        public DateTime DataServizio { get; set; }
    }
}