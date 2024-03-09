using back_end_s6_l05.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace back_end_s6_l05.Controllers
{
    public class ServiziAggiuntiviController : Controller
    {
        private List<Prenotazione> GetPrenotazioniFromDatabase(SqlConnection connection)
        {
            List<Prenotazione> listaPrenotati = new List<Prenotazione>();
            var commandList = new SqlCommand("SELECT * FROM Prenotazioni", connection);
            var readerList = commandList.ExecuteReader();

            if (readerList.HasRows)
            {
                while (readerList.Read())
                {
                    var prenotazione = new Prenotazione()
                    {
                        ID = (int)readerList["ID"],
                        Cognome = (string)readerList["Cognome"],
                        Nome = (string)readerList["Nome"],
                    };
                    listaPrenotati.Add(prenotazione);
                }
            }

            return listaPrenotati;
        }

        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Devi prima effettuare il login.";
                return View("Error");
            }

            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                ViewBag.listaPrenotati = GetPrenotazioniFromDatabase(conn);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(ServizioAggiuntivo nuovoServizio)
        {
            if (ModelState.IsValid)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                using (var conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        var command = new SqlCommand("INSERT INTO ServiziAggiuntivi (ID, Descrizione, Quantita, Prezzo, DataServizio) VALUES (@ID, @Descrizione, @Quantita, @Prezzo, @DataServizio)", conn);
                        command.Parameters.AddWithValue("@ID", nuovoServizio.ID);
                        command.Parameters.AddWithValue("@Descrizione", nuovoServizio.Descrizione);
                        command.Parameters.AddWithValue("@Quantita", nuovoServizio.Quantita);
                        command.Parameters.AddWithValue("@Prezzo", nuovoServizio.Prezzo);
                        command.Parameters.AddWithValue("@DataServizio", nuovoServizio.DataServizio);
                        var nRows = command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        return View("error");
                    }
                }

                return RedirectToAction("Logged", "Home");
            }

            // ModelState non valido
            return View();
        }

    }
}