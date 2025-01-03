using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CamereController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<CamereController> _logger;

        // Constructor pentru injectia dependentei AppDbContext si ILogger
        public CamereController(AppDbContext context, ILogger<CamereController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Metoda GET pentru a incarca formularul de gestionare a camerelor
        public async Task<IActionResult> GestionareCamereDashboard()
        {
            try
            {
                ViewBag.Camine = await _context.Camine.ToListAsync();
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la incarcarea listei de camine.");
                TempData["ErrorMessage"] = "A aparut o eroare la incarcarea datelor.";
                return View();
            }
        }

        // Metoda POST pentru a adauga o camera
        [HttpPost]
        public async Task<IActionResult> AdaugaCamera(int numarCamera, int capacitate, string stare, int caminID)
        {
            try
            {
                var camera = new Camera
                {
                    NumarCamera = numarCamera,
                    Capacitate = capacitate,
                    Stare = stare,
                    CaminID = caminID
                };

                _context.Camere.Add(camera);
                await _context.SaveChangesAsync();

                return RedirectToAction("GestionareCamereDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la adaugarea camerei.");
                TempData["ErrorMessage"] = "A aparut o eroare la adaugarea camerei.";
                return RedirectToAction("GestionareCamereDashboard");
            }
        }

        // Metoda POST pentru a modifica starea unei camere
        [HttpPost]
        public async Task<IActionResult> ModificaStareCamera(int numarCamera, string stare)
        {
            try
            {
                var camera = await _context.Camere.FirstOrDefaultAsync(c => c.NumarCamera == numarCamera);

                if (camera != null)
                {
                    camera.Stare = stare;
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Starea camerei a fost actualizata cu succes.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Camera cu acest numar nu a fost gasita.";
                }

                return RedirectToAction("GestionareCamereDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la modificarea starii camerei.");
                TempData["ErrorMessage"] = "A aparut o eroare la modificarea starii camerei.";
                return RedirectToAction("GestionareCamereDashboard");
            }
        }

        // Metoda POST pentru a sterge o camera
        [HttpPost]
        public async Task<IActionResult> StergeCamera(int cameraID)
        {
            try
            {
                var camera = await _context.Camere.FindAsync(cameraID);

                if (camera == null)
                {
                    TempData["ErrorMessage"] = "Camera nu a fost gasita.";
                    return RedirectToAction("GestionareCamereDashboard");
                }

                if (camera.Stare == "Ocupata")
                {
                    TempData["ErrorMessage"] = "Camera nu poate fi stearsa deoarece este ocupata.";
                    return RedirectToAction("GestionareCamereDashboard");
                }

                _context.Camere.Remove(camera);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Camera a fost stearsa cu succes.";
                return RedirectToAction("GestionareCamereDashboard");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eroare la stergerea camerei.");
                TempData["ErrorMessage"] = "A aparut o eroare la stergerea camerei.";
                return RedirectToAction("GestionareCamereDashboard");
            }
        }
    }
}
