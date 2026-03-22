using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PawSpital.Models;

namespace PawSpital.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View("index");
    public IActionResult Contact() => View("contact");
    public IActionResult Departamente() => View("departamente");
    public IActionResult Despre() => View("despre");
    public IActionResult Doctori() => View("doctori");
    public IActionResult Documentatie() => View("documentatie-aplicatie");
    public IActionResult Login() => View("login");
    public IActionResult Profil() => View("profil");
    public IActionResult Programari() => View("programari");
    public IActionResult Register() => View("register");
    public IActionResult Servicii() => View("servicii");
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
