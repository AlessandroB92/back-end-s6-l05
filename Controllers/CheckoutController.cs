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
    public class CheckoutController : Controller
    {

        // GET: Checkout
        public ActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Devi effettuare il login per accedere al Checkout.";
                return View("Error");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Prenotazione> prenotazioni = new List<Prenotazione>();
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
                        Nome = (string)readerList["Nome"]
                    };
                    prenotazioni.Add(prenotazione);
                }
                ViewBag.prenotazioni = prenotazioni;
                conn.Close();
            }
            return View();
        }

        public ActionResult CheckoutResult(string ID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Checkout> checkouts = new List<Checkout>();
            try
            {
                conn.Open();
                var commandList = new SqlCommand("SELECT c.Numero, p.PeriodoInizio, p.PeriodoFine, s.Descrizione, s.DataServizio, (SUM(p.Tariffa - p.Caparra) + SUM(s.Prezzo)) AS TotaleImporto FROM Prenotazioni as p JOIN ServiziAggiuntivi AS s ON s.ID = p.ID JOIN Camere AS c ON c.Numero = p.NumeroCamera WHERE p.ID = @ID GROUP BY c.Numero, p.PeriodoInizio, p.PeriodoFine, s.Descrizione, s.DataServizio", conn);
                commandList.Parameters.AddWithValue("@ID", ID);
                var readerList = commandList.ExecuteReader();

                if (readerList.HasRows)
                {
                    while (readerList.Read())
                    {
                        var checkout = new Checkout()
                        {
                            NumeroCamera = (int)readerList["Numero"],
                            PeriodoInizio = (DateTime)readerList["PeriodoInizio"],
                            PeriodoFine = (DateTime)readerList["PeriodoFine"],
                            Descrizione = (string)readerList["Descrizione"],
                            DataServizio = (DateTime)readerList["DataServizio"],
                        };
                        checkouts.Add(checkout);
                    }
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
            conn.Close();
            return View(checkouts);
        }
    }
}