using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawSpital.Models;
using PawSpital.Models.Auth;
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

    public IActionResult Index() => View("index");
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

    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction(nameof(Profil));
        return View("login", new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return View("login", model);

        var result = await _authService.LoginAsync(model.Email, model.Password);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error);
            return View("login", model);
        }

        return RedirectToAction(nameof(Profil));
    }

    [Authorize]
    public async Task<IActionResult> Profil()
    {
        var user = await _authService.GetCurrentUserAsync(User);
        if (user == null)
            return RedirectToAction(nameof(Login));

        var roles = await _authService.GetUserRolesAsync(User);

        ViewData["ProfileEmail"] = user.Email;
        ViewData["ProfileName"] = user.FullName;
        ViewData["ProfileRole"] = string.Join(", ", roles);
        ViewData["ProfileImage"] = user.ProfileImagePath;
        return View("profil");
    }

    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateProfileImage(IFormFile profileImage)
    {
        var result = await _authService.UpdateProfileImageAsync(User, profileImage);
        if (!result.Success)
            TempData["ErrorMessage"] = result.Error;
        else
            TempData["SuccessMessage"] = "Imaginea de profil a fost actualizată cu succes!";

        return RedirectToAction(nameof(Profil));
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

    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction(nameof(Profil));
        return View("register", new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View("register", model);

        var result = await _authService.RegisterAsync(model.FullName, model.Email, model.Password);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error);
            return View("register", model);
        }

        // Auto-login after registration
        await _authService.LoginAsync(model.Email, model.Password);
        return RedirectToAction(nameof(Profil));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _authService.LogoutAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Servicii()
    {
        var model = await _serviciuService.GetAllAsync();
        return View("servicii", model);
    }

    public IActionResult AccessDenied() => View("AccessDenied");

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
