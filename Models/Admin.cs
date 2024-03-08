using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace back_end_s6_l05.Models
{
    public class Admin
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Il nome utente è obbligatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "La password è obbligatoria")]
        public string Password { get; set; }
    }
}