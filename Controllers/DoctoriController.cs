using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawSpital.Models;
using PawSpital.Security;
using PawSpital.Services;

namespace PawSpital.Controllers;

public class DoctoriController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IDepartamentService _departamentService;

    public DoctoriController(IDoctorService doctorService, IDepartamentService departamentService)
    {
        _doctorService = doctorService;
        _departamentService = departamentService;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _doctorService.GetAllAsync();
        return View(list);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var doctor = await _doctorService.GetByIdAsync(id.Value);
        if (doctor is null) return NotFound();

        return View(doctor);
    }

    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Create()
    {
        await PopulateDepartamenteAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Create([Bind("Nume,Specializare,DepartamentId")] Doctor model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateDepartamenteAsync(model.DepartamentId);
            return View(model);
        }

        await _doctorService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var doctor = await _doctorService.GetByIdAsync(id.Value);
        if (doctor is null) return NotFound();

        await PopulateDepartamenteAsync(doctor.DepartamentId);
        return View(doctor);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Specializare,DepartamentId")] Doctor model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateDepartamenteAsync(model.DepartamentId);
            return View(model);
        }

        var updated = await _doctorService.UpdateAsync(model);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var doctor = await _doctorService.GetByIdAsync(id.Value);
        if (doctor is null) return NotFound();

        return View(doctor);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _doctorService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateDepartamenteAsync(int? selectedId = null)
    {
        var departamente = await _departamentService.GetAllAsync();
        ViewBag.DepartamentId = new SelectList(departamente, "Id", "Nume", selectedId);
    }
}

