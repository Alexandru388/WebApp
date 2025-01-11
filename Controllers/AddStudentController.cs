using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace WebApplication1.Controllers;

public class AddStudentController: Controller
{
    private readonly AppDbContext _context;
    private readonly ILogger<AddStudentController> _logger;

    // Constructorul modificat pentru a include ILogger
    public AddStudentController(AppDbContext context, ILogger<AddStudentController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public IActionResult AddStudentDashboard()
    {
        _logger.LogInformation("Cautam formularul de adaugare student.");
        return View();
    }
    [HttpPost]
    public IActionResult Create(Student student)
    {
        _logger.LogInformation("Se incearca adaugarea unui camin cu numele: {Nume}", student.Nume);

        if (ModelState.IsValid)
        {
            try
            {
                _logger.LogInformation("Modelul este valid. Se adauga studentul in baza de date.");
                _context.Studenti.Add(student);
                _context.SaveChanges();
                _logger.LogInformation("Caminul {Nume} a fost adaugat cu succes.", student.Nume);
                return RedirectToAction("AddStudentDashboard", "AddStudent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "A aparut o eroare la salvarea studentului {Nume}.", student.Nume);
                return View("AddStudentDashboard");
            }
        }
        else
        {
            _logger.LogWarning("Modelul nu este valid pentru studentul {Nume}.", student.Nume);
            return View("AddStudentDashboard");
        }
    }
    public IActionResult AdaugareStudent(Student student)
    {
        if (string.IsNullOrEmpty(student.Nume) || string.IsNullOrEmpty(student.CNP) || string.IsNullOrEmpty(student.NumarMatricol)|| decimal.IsNegative(student.NotaCazare))
        {
            return View("AddStudentDashboard"); // Returneaza mesaj de eroare
        }

        // Executa un query SQL folosind SqliteParameter pentru SQLite
        string sql = "INSERT INTO Studenti (Nume, CNP, NumarMatricol, NotaCazare) VALUES (@Nume, @CNP, @NumarMatricol, @NotaCazare)";

        // Foloseste SqliteParameter in loc de SqlParameter
        _context.Database.ExecuteSqlRaw(sql,
            new SqliteParameter("@Nume", student.Nume),
            new SqliteParameter("@CNP", student.CNP),
            new SqliteParameter("@NumarMatricol", student.NumarMatricol),
            new SqliteParameter("@NotaCazare", student.NotaCazare)
        );

        return RedirectToAction("AddStudentDashboard", "AddStudent");
    }

    public IActionResult StergereStudent(Student studentID)
    {
        return View(studentID);
    }
    public IActionResult AfisareStudenti()
    {
        var studenti = _context.Studenti
            .Select(st => new StudentViewModel
            {
                Nume = st.Nume,
                CNP = st.CNP,
                NumarMatricol = st.NumarMatricol,
                NotaCazare = st.NotaCazare
            })
            .ToList();

        return View(studenti);
    }
}