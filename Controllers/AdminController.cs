using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using back_end_s6_l05.Models;

namespace back_end_s6_l05.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Logged");
            }
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin admin)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["HotelDb"].ToString();
                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var command = new SqlCommand("SELECT * FROM Admin WHERE Username = @username AND Password = @password", conn);
                        command.Parameters.AddWithValue("@username", admin.Username);
                        command.Parameters.AddWithValue("@password", admin.Password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                FormsAuthentication.SetAuthCookie(reader["ID"].ToString(), true);
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                // Imposta un messaggio di errore e reindirizza alla pagina di login
                                TempData["ErrorMessage"] = "Credenziali errate. Riprova.";
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Gestisci l'errore in modo appropriato, ad esempio, registralo o mostra un messaggio all'utente
                    ModelState.AddModelError("", "Si è verificato un errore durante il login.");
                    return View(admin);
                }
            }
            return View(admin);
        }

        [Authorize]
        public ActionResult Logged()
        {
            var ID = HttpContext.User.Identity.Name;
            ViewBag.ID = ID;
            return View();
        }

        [Authorize, HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Admin");
        }
    }
}