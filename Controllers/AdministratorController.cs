using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers   
{
    public class AdministratorController : Controller
    {
        private readonly AppDbContext _context;

        public AdministratorController(AppDbContext context)
        {
            _context = context;
        }

        // Acțiune pentru a obține administratorii din baza de date
       
        public async Task<IActionResult> List()
        {
            var administratori = await _context.Administratori.ToListAsync();

            if (!administratori.Any())
            {
                // Dacă nu sunt administratori, trimite un mesaj că lista este goală
                ViewData["Message"] = "Nu există administratori.";
            }
            else
            {
                // Altfel, trimite lista administratorilor
                ViewData["Message"] = null; // Poți seta mesajul la null sau la un alt mesaj
            }

            return View(administratori);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string parola)
        {
            // Caută administratorul cu email-ul și parola specificate
            var admin = await _context.Administratori
                .FirstOrDefaultAsync(a => a.Email == email && a.Parola == parola);

            if (admin != null)
            {
                // Dacă autentificarea este reușită
                // Poți seta o sesiune, un cookie sau redirecționa utilizatorul către altă pagină
                return RedirectToAction("Index", "Home"); // Exemplu de redirecționare
            }
            else
            {
                // Dacă autentificarea eșuează, afișează un mesaj de eroare
                ViewData["ErrorMessage"] = "Email-ul sau parola sunt incorecte.";
                return View();
            }
        }




    }
}