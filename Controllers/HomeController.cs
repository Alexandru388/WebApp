using System.Diagnostics; 
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    public IActionResult Index()
    {
        // Mesajul pe care vrem sa il trimitem la view
        ViewData["Message"] = "Bun venit la aplicatia noastra MVC!";
        return View();
    }
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Metoda pentru a afisa pagina de Counter
    public IActionResult Counter()
    {
        return View();
    }

    // Metoda pentru a afisa pagina de Login
    public IActionResult Login()
    {
        return View();
    }   
    
    // Metoda pentru a afisa pagina de Privacy
    public IActionResult Privacy()
    {
        return View();
    }

    // Metoda pentru a gestiona erorile
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}