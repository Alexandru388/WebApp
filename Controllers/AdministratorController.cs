using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdministratorController> _logger;

        public AdministratorController(AppDbContext context, ILogger<AdministratorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Actiune pentru a obtine administratorii din baza de date
        public async Task<IActionResult> List()
        {
            var administratori = await _context.Administratori.ToListAsync();

            if (!administratori.Any())
            {
                // Logare ca nu exista administratori
                _logger.LogWarning("Nu exista administratori in baza de date.");

                // Daca nu sunt administratori, trimite un mesaj
                ViewData["Message"] = "Nu exista administratori.";
            }
            else
            {
                // Logare ca au fost gasiti administratori
                _logger.LogInformation($"Găsiti {administratori.Count} administratori in baza de date.");

                // Seteaza mesajul ca null
                ViewData["Message"] = null;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string parola)
        {
            // Logare incercare de autentificare
            _logger.LogInformation($"Tentativa de autentificare pentru email: {email}");

            // Cauta administratorul cu email-ul si parola specificate
            var admin = await _context.Administratori
                .FirstOrDefaultAsync(a => a.Email == email && a.Parola == parola);

            if (admin != null)
            {
                // Logare autentificare reusita
                _logger.LogInformation($"Autentificare reusita pentru {email}.");

                return RedirectToAction("CaminDashboard", "Camine");  // Redirectionare
            }
            else
            {
                // Logare autentificare esuata
                _logger.LogWarning($"Autentificare esuata pentru {email}. Parola sau email-ul incorecte.");

                // Afișează mesaj de eroare
                ViewData["ErrorMessage"] = "Email-ul sau parola sunt incorecte.";
                return View();
            }
        }
    }
}