using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AddStudentsController : Controller
    {
        private readonly AppDbContext _context;

        public AddStudentsController(AppDbContext context)
        {
            _context = context;
        }

        // Display the form for adding a student
        [HttpGet]
        public IActionResult AddStudentForm()
        {
            return View();
        }

        // Handle the form submission to add a student
        [HttpPost]
        public async Task<IActionResult> AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                // Add the student to the database
                _context.Studenti.Add(student);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or the form again
                return RedirectToAction("StudentList");
            }

            // If validation fails, reload the form with validation errors
            return View("AddStudentForm", student);
        }

        // Display the list of all students
        [HttpGet]
        public async Task<IActionResult> StudentList()
        {
            var students = await _context.Studenti.ToListAsync();
            return View(students);
        }
    }
}