using Microsoft.AspNetCore.Mvc;
using PawSpital.Models;
using PawSpital.Security;
using PawSpital.Services;

namespace PawSpital.Controllers;

[RequireRole(AppRoles.Admin)]
public class AdminController : Controller
{
    private readonly IDepartamentService _departamentService;
    private readonly IDoctorService _doctorService;
    private readonly IServiciuService _serviciuService;
    private readonly ISalaService _salaService;
    private readonly IProgramareService _programareService;

    public AdminController(
        IDepartamentService departamentService,
        IDoctorService doctorService,
        IServiciuService serviciuService,
        ISalaService salaService,
        IProgramareService programareService)
    {
        _departamentService = departamentService;
        _doctorService = doctorService;
        _serviciuService = serviciuService;
        _salaService = salaService;
        _programareService = programareService;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeDashboardViewModel
        {
            DepartamenteCount = (await _departamentService.GetAllAsync()).Count,
            DoctoriCount = (await _doctorService.GetAllAsync()).Count,
            ServiciiCount = (await _serviciuService.GetAllAsync()).Count,
            SaliCount = (await _salaService.GetAllAsync()).Count,
            ProgramariCount = (await _programareService.GetAllAsync()).Count
        };

        return View(model);
    }
}
