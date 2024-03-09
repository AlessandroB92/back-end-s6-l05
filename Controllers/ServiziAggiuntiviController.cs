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
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Prenotazione> listaPrenotati = new List<Prenotazione>();
            conn.Open();
            var commandList = new SqlCommand("SELECT * FROM Prenotazioni", conn);
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
                ViewBag.listaPrenotati = listaPrenotati;
                conn.Close();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(ServizioAggiuntivo nuovoServizio)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Prenotazione> listaPrenotati = new List<Prenotazione>();
            conn.Open();
            var commandList = new SqlCommand("SELECT * FROM Prenotazioni", conn);
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
                ViewBag.listaPrenotati = listaPrenotati;
                conn.Close();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    conn.Open();
                    var command = new SqlCommand("INSERT INTO ServiziAggiuntivi (PrenotazioneID, Descrizione, Quantita, Prezzo, DataServizio,) VALUES (@PrenotazioneID, @Descrizione, @Quantita, @Prezzo, @DataServizio)", conn);
                    command.Parameters.AddWithValue("@PrenotazioneID", nuovoServizio.PrenotazioneID);
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
                finally { conn.Close(); }
                return RedirectToAction("Logged", "Home");
            }
            return View();
        }
    }
}