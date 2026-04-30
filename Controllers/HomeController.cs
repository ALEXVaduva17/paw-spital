using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawSpital.Models;
using PawSpital.Models.Auth;
using PawSpital.Security;
using PawSpital.Services;

namespace PawSpital.Controllers;

public class HomeController : Controller
{
    private readonly IDepartamentService _departamentService;
    private readonly IDoctorService _doctorService;
    private readonly IServiciuService _serviciuService;
    private readonly ISalaService _salaService;
    private readonly IProgramareService _programareService;
    private readonly IAuthService _authService;

    public HomeController(
        IDepartamentService departamentService,
        IDoctorService doctorService,
        IServiciuService serviciuService,
        ISalaService salaService,
        IProgramareService programareService,
        IAuthService authService)
    {
        _departamentService = departamentService;
        _doctorService = doctorService;
        _serviciuService = serviciuService;
        _salaService = salaService;
        _programareService = programareService;
        _authService = authService;
    }

    public IActionResult Index()
    {
        return View("index");
    }

    public IActionResult Contact() => View("contact");
    public async Task<IActionResult> Departamente()
    {
        var model = await _departamentService.GetAllAsync();
        return View("departamente", model);
    }
    public IActionResult Despre() => View("despre");
    public async Task<IActionResult> Doctori()
    {
        var model = await _doctorService.GetAllAsync();
        return View("doctori", model);
    }
    public IActionResult Documentatie() => View("documentatie-aplicatie");
    public IActionResult Login() => View("login", new LoginViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View("login", model);

        if (!_authService.ValidateCredentials(model.Email, model.Password, out var fullName, out var role))
        {
            ModelState.AddModelError(string.Empty, "Email sau parolă invalidă.");
            return View("login", model);
        }

        HttpContext.Session.SetString("UserEmail", model.Email.Trim().ToLowerInvariant());
        HttpContext.Session.SetString("UserFullName", fullName);
        HttpContext.Session.SetString("UserRole", role);
        return RedirectToAction(nameof(Profil));
    }

    public IActionResult Profil()
    {
        var email = HttpContext.Session.GetString("UserEmail");
        var fullName = HttpContext.Session.GetString("UserFullName");
        var role = HttpContext.Session.GetString("UserRole");
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(role))
            return RedirectToAction(nameof(Login));

        ViewData["ProfileEmail"] = email;
        ViewData["ProfileName"] = fullName;
        ViewData["ProfileRole"] = role;
        return View("profil");
    }

    public async Task<IActionResult> Programari()
    {
        await PopulateProgramareListsAsync();
        return View("programari", new PublicProgramareViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Programari(PublicProgramareViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateProgramareListsAsync(model.DoctorId, model.ServiciuId);
            return View("programari", model);
        }

        var programare = new Programare
        {
            NumePacient = model.NumePacient.Trim(),
            Telefon = model.Telefon.Trim(),
            DoctorId = model.DoctorId,
            ServiciuId = model.ServiciuId,
            Data = model.Data,
            Status = "In asteptare"
        };

        await _programareService.CreateAsync(programare);
        TempData["SuccessMessage"] = "Programarea a fost trimisa cu succes.";
        return RedirectToAction(nameof(Programari));
    }

    public IActionResult Register() => View("register", new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View("register", model);

        var result = _authService.Register(model.FullName, model.Email, model.Password);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error);
            return View("register", model);
        }

        HttpContext.Session.SetString("UserEmail", model.Email.Trim().ToLowerInvariant());
        HttpContext.Session.SetString("UserFullName", model.FullName.Trim());
        HttpContext.Session.SetString("UserRole", AppRoles.Pacient);
        return RedirectToAction(nameof(Profil));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Servicii()
    {
        var model = await _serviciuService.GetAllAsync();
        return View("servicii", model);
    }
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private async Task PopulateProgramareListsAsync(int? doctorId = null, int? serviciuId = null)
    {
        var doctori = await _doctorService.GetAllAsync();
        var servicii = await _serviciuService.GetAllAsync();
        ViewBag.DoctorId = new SelectList(doctori, "Id", "Nume", doctorId);
        ViewBag.ServiciuId = new SelectList(servicii, "Id", "Nume", serviciuId);
    }
}
