using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace back_end_s6_l05.Models
{
    public class Prenotazione
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri")]
        public string CodiceFiscale { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Il cognome deve essere lungo almeno 2 caratteri")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Il nome deve essere lungo almeno 2 caratteri")]
        public string Nome { get; set; }

        public string Citta { get; set; }

        [StringLength(2, MinimumLength = 2)]
        public string Provincia { get; set; }

        [EmailAddress(ErrorMessage = "Inserisci un indirizzo email valido")]
        public string Email { get; set; }

        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Il numero di telefono non è valido")]
        public int? Telefono { get; set; }

        [Required(ErrorMessage = "Il numero di cellulare è obbligatorio")]
        [RegularExpression(@"^[0-9]{0,15}$", ErrorMessage = "Il numero di cellulare non è valido")]
        public int Cellulare { get; set; }

        [Required(ErrorMessage = "Il numero della camera è obbligatorio")]
        public int NumeroCamera { get; set; }

        public DateTime DataPrenotazione { get; set; }
        public int Anno { get; set; }
        public DateTime PeriodoInizio { get; set; }
        public DateTime PeriodoFine { get; set; }
        public decimal? Caparra { get; set; }
        public decimal Tariffa { get; set; }
        public string TipoSoggiorno { get; set; }

        public Camera Camera { get; set; }
    }
}