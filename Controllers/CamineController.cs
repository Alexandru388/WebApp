using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite; // Asigură-te că folosești Sqlite pentru SQLite
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers
{
    public class CamineController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CamineController> _logger;

        // Constructorul modificat pentru a include ILogger
        public CamineController(AppDbContext context, ILogger<CamineController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Vizualizarea formularului pentru adăugarea unui cămin
        public IActionResult CaminDashboard()
        {
            _logger.LogInformation("Căutăm formularul de adăugare cămin.");
            return View();
        }

        // Metoda pentru a adăuga un cămin
        [HttpPost]
        public IActionResult Create(Camin camin)
        {
            _logger.LogInformation("Se încearcă adăugarea unui cămin cu numele: {Nume}", camin.Nume);

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Modelul este valid. Se adaugă căminul în baza de date.");
                    _context.Camine.Add(camin);
                    _context.SaveChanges();
                    _logger.LogInformation("Căminul {Nume} a fost adăugat cu succes.", camin.Nume);
                    return RedirectToAction("CaminDashboard", "Camine");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A apărut o eroare la salvarea căminului {Nume}.", camin.Nume);
                    return View("CaminDashboard");
                }
            }
            else
            {
                _logger.LogWarning("Modelul nu este valid pentru căminul {Nume}.", camin.Nume);
                return View("CaminDashboard");
            }
        }

        // Metoda modificată pentru a utiliza SqliteParameter
      public IActionResult CreateCaminManual(Camin2 camin)
{
    if (string.IsNullOrEmpty(camin.Nume) || string.IsNullOrEmpty(camin.Adresa) || camin.NumarCamere <= 0 || camin.UniversitateID <= 0)
    {
        return View("CaminDashboard", camin); // Poți returna un mesaj de eroare
    }

    // Execută un query SQL folosind SqliteParameter pentru SQLite
    string sql = "INSERT INTO Camine (Nume, Adresa, NumarCamere, UniversitateID) VALUES (@Nume, @Adresa, @NumarCamere, @UniversitateID)";

    // Folosește SqliteParameter în loc de SqlParameter
    _context.Database.ExecuteSqlRaw(sql,
        new SqliteParameter("@Nume", camin.Nume),
        new SqliteParameter("@Adresa", camin.Adresa),
        new SqliteParameter("@NumarCamere", camin.NumarCamere),
        new SqliteParameter("@UniversitateID", camin.UniversitateID)
    );

    return RedirectToAction("CaminDashboard", "Camine");
}

    }
}
