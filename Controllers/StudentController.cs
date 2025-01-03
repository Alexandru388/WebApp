using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // Metoda pentru afisarea formularului de login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Metoda pentru procesarea formularului de login
        [HttpPost]
        public async Task<IActionResult> Login(string nume, string numarMatricol)
        {
            // Verifica in baza de date daca studentul exista
            var student = await _context.Studenti
                .FirstOrDefaultAsync(s => s.Nume == nume && s.NumarMatricol == numarMatricol);

            if (student != null)
            {
                // Daca autentificarea reuseste
                ViewData["Message"] = $"Bun venit, {student.Nume}!";
                TempData["StudentNume"] = student.Nume;
                return RedirectToAction("Dashboard", "Student"); // Redirectioneaza catre o alta actiune
            }
            else
            {
                // Daca autentificarea esueaza
                ViewData["ErrorMessage"] = "Nume sau numar matricol incorect.";
                return View();
            }
        }

        // Dashboard sau pagina principala a studentului
        public async Task<IActionResult> Dashboard()
        {
            var numeStudent = TempData["StudentNume"] as string;

            var query = @"
                SELECT
                    C.Nume AS NumeCamin,
                    Cam.NumarCamera,
                    (Cam.Capacitate - COUNT(Cz.StudentID)) AS NumarColegi
                FROM
                    Studenti S
                    JOIN Cazari Cz ON S.StudentID = Cz.StudentID
                    JOIN Camere Cam ON Cz.CameraID = Cam.CameraID
                    JOIN Camine C ON Cam.CaminID = C.CaminID
                WHERE
                    S.Nume = {0}
                GROUP BY
                    C.Nume,
                    Cam.NumarCamera,
                    Cam.Capacitate;
            ";

            // Executa query-ul si maparea la CazareInfo
            var cazareInfo = await _context
                .Set<CazareInfo>()
                .FromSqlRaw(query, numeStudent)
                .ToListAsync();

            return View(cazareInfo);
        }
    }
}
