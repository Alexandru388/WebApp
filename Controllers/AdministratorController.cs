using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AdministratorController> _logger;
        private readonly PasswordHasher<Administrator> _passwordHasher;

        // Constructorul cu injectarea dependențelor
        public AdministratorController(AppDbContext context, ILogger<AdministratorController> logger)
        {
            _context = context;
            _logger = logger;
            _passwordHasher = new PasswordHasher<Administrator>();  // Inițializezi PasswordHasher
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

            return View(administratori);
        }

        // Metoda pentru a accesa formularul de login
      [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string parola)
        {
            _logger.LogInformation($"Tentativa de autentificare pentru email: {email}");

            var admin = await _context.Administratori
                .FirstOrDefaultAsync(a => a.Email == email);

            if (admin != null)
            {
                // Verificăm parola cu hash-ul stocat
                bool isPasswordCorrect = VerifyPassword(admin.Parola, parola);  // Verificare

                if (isPasswordCorrect)
                {
                    _logger.LogInformation($"Autentificare reușită pentru {email}.");
                    return View("AdminDashboard");
                }
                else
                {
                    _logger.LogWarning($"Autentificare eșuată pentru {email}. Parola incorectă.");
                    ViewData["ErrorMessage"] = "Parola este incorectă.";
                }
            }
            else
            {
                _logger.LogWarning($"Autentificare eșuată pentru {email}. Email-ul nu există.");
                ViewData["ErrorMessage"] = "Email-ul nu există.";
            }

            return View();
        }

        // Metodă pentru a verifica parola criptată
        private bool VerifyPassword(string storedHash, string inputPassword)
        {
            // Criptăm parola introdusă de utilizator
            string inputHash = ComputeSha256Hash(inputPassword);

            // Comparăm hash-ul stocat cu cel introdus
            return storedHash.Equals(inputHash, StringComparison.OrdinalIgnoreCase);
        }

        // Metodă pentru criptarea parolei folosind SHA256
        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Aplicăm SHA256 asupra parolei
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convertim byte[] la string hexazecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
