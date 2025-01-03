using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
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

        // Vizualizarea formularului pentru adaugarea unui camin
        public IActionResult CaminDashboard()
        {
            _logger.LogInformation("Cautam formularul de adaugare camin.");
            return View();
        }

        // Metoda pentru a adauga un camin
        [HttpPost]
        public IActionResult Create(Camin camin)
        {
            _logger.LogInformation("Se incearca adaugarea unui camin cu numele: {Nume}", camin.Nume);

            if (ModelState.IsValid)
            {
                try
                {
                    _logger.LogInformation("Modelul este valid. Se adauga caminul in baza de date.");
                    _context.Camine.Add(camin);
                    _context.SaveChanges();
                    _logger.LogInformation("Caminul {Nume} a fost adaugat cu succes.", camin.Nume);
                    return RedirectToAction("CaminDashboard", "Camine");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "A aparut o eroare la salvarea caminului {Nume}.", camin.Nume);
                    return View("CaminDashboard");
                }
            }
            else
            {
                _logger.LogWarning("Modelul nu este valid pentru caminul {Nume}.", camin.Nume);
                return View("CaminDashboard");
            }
        }

        // Metoda pentru a crea un camin manual folosind SqliteParameter
        public IActionResult CreateCaminManual(Camin2 camin)
        {
            if (string.IsNullOrEmpty(camin.Nume) || string.IsNullOrEmpty(camin.Adresa) || camin.NumarCamere <= 0 || camin.UniversitateID <= 0)
            {
                return View("CaminDashboard", camin); // Returneaza mesaj de eroare
            }

            // Executa un query SQL folosind SqliteParameter pentru SQLite
            string sql = "INSERT INTO Camine (Nume, Adresa, NumarCamere, UniversitateID) VALUES (@Nume, @Adresa, @NumarCamere, 1)";

            // Foloseste SqliteParameter in loc de SqlParameter
            _context.Database.ExecuteSqlRaw(sql,
                new SqliteParameter("@Nume", camin.Nume),
                new SqliteParameter("@Adresa", camin.Adresa),
                new SqliteParameter("@NumarCamere", camin.NumarCamere)
            );

            return RedirectToAction("CaminDashboard", "Camine");
        }

        // Metoda pentru a afisa caminele
        public IActionResult AfisareCamine()
        {
            var camine = _context.Camine
                .Include(c => c.Camere) // Include relatia cu Camere
                .Select(c => new CaminViewModel
                {
                    CaminID = c.CaminID,
                    Nume = c.Nume,
                    Adresa = c.Adresa,
                    CamereLibere = c.Camere.Count(cam => cam.Stare == "Disponibila") // Numara camerele libere
                })
                .ToList();

            return View(camine);
        }

        // Metoda pentru a afisa caminele ordonate dupa nume
        public async Task<IActionResult> AfisareCamineOrdonate()
        {
            // Obține caminele ordonate dupa nume
            var camine = await _context.Camine
                .OrderBy(c => c.Nume)
                .ToListAsync();

            return View(camine);
        }

        // Metoda pentru a afisa caminele ordonate dupa numarul de camere disponibile
        public async Task<IActionResult> AfisareCamineOrdonateDupaCamere()
        {
            var camine = await _context.Camine
                .Select(c => new 
                {
                    c.CaminID,
                    c.Nume,
                    c.Adresa,
                    NumarCamereDisponibile = c.Camere.Count(cam => cam.Stare == "Disponibila")
                })
                .OrderByDescending(c => c.NumarCamereDisponibile)
                .ToListAsync();

            // Mapam la un model de View daca este nevoie
            var viewModel = camine.Select(c => new CaminViewModel
            {
                CaminID = c.CaminID,
                Nume = c.Nume,
                Adresa = c.Adresa,
                CamereLibere = c.NumarCamereDisponibile
            });

            return View(viewModel);
        }
    }
}
