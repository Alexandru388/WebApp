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
       
        public async Task<IActionResult> Index()
        {
            // Obține lista administratorilor din baza de date
            var administratori = await _context.Administratori.ToListAsync();

            // Verifică dacă lista este null sau goală
            if (administratori == null || !administratori.Any())
            {
                // Dacă nu sunt administratori, poți adăuga un mesaj de eroare sau o acțiune
                return View("Index");  // Specifică explict că vrei să folosești Index.cshtml din Views/Home/
            }

            // Trimite lista de administratori la view
            return View("Index", administratori);  // Specifică explict că vrei să folosești Index.cshtml din Views/Home/
        }


    }
}