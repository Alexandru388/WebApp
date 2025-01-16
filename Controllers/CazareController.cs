using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging; // Adaugat pentru logger
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CazareController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CazareController> _logger; // Declarat logger

        public CazareController(AppDbContext context, ILogger<CazareController> logger)
        {
            _context = context;
            _logger = logger; // Initializat logger
        }

        [HttpGet]
        public IActionResult Atribuire()
        {
            _logger.LogInformation("Afisare pagina Atribuire"); // Logare mesaj
            ViewBag.Studenti = _context.Studenti.ToList();
            ViewBag.Camine = _context.Camine.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Atribuire(int studentId, int caminId, int numarCamera)
        {
            _logger.LogInformation("Incepere proces Atribuire pentru studentul {studentId}", studentId); // Logare mesaj cu parametru

            var student = _context.Studenti.FirstOrDefault(s => s.StudentID == studentId);
            if (student == null)
            {
                ModelState.AddModelError("", "Studentul nu exista.");
                _logger.LogWarning("Studentul cu ID {studentId} nu a fost gasit.", studentId); // Logare avertizare
                return View();
            }

            var camin = _context.Camine.FirstOrDefault(c => c.CaminID == caminId);
            if (camin == null)
            {
                ModelState.AddModelError("", "Caminul nu exista.");
                _logger.LogWarning("Caminul cu ID {caminId} nu a fost gasit.", caminId); // Logare avertizare
                return View();
            }

            var camera = _context.Camere
                .FirstOrDefault(c => c.CaminID == caminId && c.NumarCamera == numarCamera && c.Stare == "Disponibila");

            if (camera == null)
            {
                ModelState.AddModelError("", "Camera specificata nu exista sau nu este disponibila.");
                _logger.LogWarning("Camera {numarCamera} din Caminul {caminId} nu este disponibila.", numarCamera, caminId); // Logare avertizare
                return View();
            }

            var capacitateOcupata = _context.Cazari
                .Include(c => c.Camera)
                .Count(c => c.Camera.CameraID == camera.CameraID);

            if (capacitateOcupata >= camera.Capacitate)
            {
                ModelState.AddModelError("", "Capacitatea camerei a fost atinsa.");
                _logger.LogWarning("Capacitatea camerei {numarCamera} a fost atinsa.", numarCamera); // Logare avertizare
                return View();
            }

            var cazare = new Cazare
            {
                Student = student,
                Camera = camera    
            };

            _context.Cazari.Add(cazare);

            camera.Stare = "Ocupata";
            _context.SaveChanges();

            _logger.LogInformation("Studentul cu ID {studentId} a fost cazat cu succes in camera {numarCamera}.", studentId, numarCamera); // Logare informatie
            TempData["Success"] = "Studentul a fost cazat cu succes!";
            return RedirectToAction("Atribuire");
        }
    }
}