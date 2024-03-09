using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using back_end_s6_l05.Models;

    namespace back_end_s6_l05.Controllers
    {
        public class PrenotazioniController : Controller
        {
        public ActionResult ElencoPrenotazioni()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.ErrorMessage = "Devi effettuare il login per visualizzare le Prenotazioni.";
                return View("Error");
            }
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Prenotazione> prenotazioni = new List<Prenotazione>();

            try
            {
                conn.Open();
                var command = new SqlCommand("SELECT * FROM Prenotazioni", conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var prenotazione = new Prenotazione()
                    {
                        ID = (int)reader["ID"],
                        CodiceFiscale = (string)reader["CodiceFiscale"],
                        Cognome = (string)reader["Cognome"],
                        Nome = (string)reader["Nome"],
                        Citta = (string)reader["Citta"],
                        Provincia = (string)reader["Provincia"],
                        Email = (string)reader["Email"],
                        Telefono = reader["Telefono"] != DBNull.Value ? (int)reader["Telefono"] : (int?)null,
                        Cellulare = (int)reader["Cellulare"],
                        NumeroCamera = (int)reader["NumeroCamera"],
                        DataPrenotazione = (DateTime)reader["DataPrenotazione"],
                        Anno = (int)reader["Anno"],
                        PeriodoInizio = (DateTime)reader["PeriodoInizio"],
                        PeriodoFine = (DateTime)reader["PeriodoFine"],
                        Caparra = reader["Caparra"] != DBNull.Value ? (decimal)reader["Caparra"] : (decimal?)null,
                        Tariffa = (decimal)reader["Tariffa"],
                        TipoSoggiorno = (string)reader["TipoSoggiorno"]
                    };
                    prenotazioni.Add(prenotazione);
                }
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Errore durante il recupero delle prenotazioni.";
                return View("Error");
            }
            finally
            {
                conn.Close();
            }

            return View(prenotazioni);
        }

        public ActionResult Index()
            {
                if (!HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewBag.ErrorMessage = "Devi effettuare il login per registrare una Prenotazione.";
                    return View("Error");
                }
            string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
            var conn = new SqlConnection(connectionString);
            List<Camera> listaCamere = new List<Camera>();
            conn.Open();
            var commandList = new SqlCommand("SELECT * FROM Camere", conn);
            var readerList = commandList.ExecuteReader();

            if (readerList.HasRows)
            {
                while (readerList.Read())
                {
                    var camera = new Camera()
                    {
                        Numero = (int)readerList["Numero"],
                        Descrizione = (string)readerList["Descrizione"],
                        Tipologia = (string)readerList["Tipologia"]
                    };
                    listaCamere.Add(camera);
                }
                ViewBag.listaCamere = listaCamere;
                conn.Close();
            }
            return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Index(Prenotazione prenotazione)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                var conn = new SqlConnection(connectionString);
                if (ModelState.IsValid)
                {
                    try
                    {
                        conn.Open();
                        var command = new SqlCommand("INSERT INTO Prenotazioni (CodiceFiscale, Cognome, Nome, Citta, Provincia, Email, Telefono, Cellulare, NumeroCamera, DataPrenotazione, Anno, PeriodoInizio, PeriodoFine, Caparra, Tariffa, TipoSoggiorno) VALUES (@CodiceFiscale, @Cognome, @Nome, @Citta, @Provincia, @Email, @Telefono, @Cellulare, @NumeroCamera, @DataPrenotazione, @Anno, @PeriodoInizio, @PeriodoFine, @Caparra, @Tariffa, @TipoSoggiorno)", conn);
                        command.Parameters.AddWithValue("@CodiceFiscale", prenotazione.CodiceFiscale);
                        command.Parameters.AddWithValue("@Cognome", prenotazione.Cognome);
                        command.Parameters.AddWithValue("@Nome", prenotazione.Nome);
                        command.Parameters.AddWithValue("@Citta", prenotazione.Citta);
                        command.Parameters.AddWithValue("@Provincia", prenotazione.Provincia);
                        command.Parameters.AddWithValue("@Email", prenotazione.Email);
                        command.Parameters.AddWithValue("@Telefono", prenotazione.Telefono);
                        command.Parameters.AddWithValue("@Cellulare", prenotazione.Cellulare);
                        command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                        command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                        command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                        command.Parameters.AddWithValue("@PeriodoInizio", prenotazione.PeriodoInizio);
                        command.Parameters.AddWithValue("@PeriodoFine", prenotazione.PeriodoFine);
                        command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                        command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                        command.Parameters.AddWithValue("@TipoSoggiorno", prenotazione.TipoSoggiorno);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {
                        ViewBag.ErrorMessage = "Errore nella compilazione dei campi.";
                        return View("Error");
                    }
                    finally { conn.Close(); }
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
        }
    }