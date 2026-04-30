using Microsoft.AspNetCore.Mvc;
using PawSpital.Models;
using PawSpital.Security;
using PawSpital.Services;

namespace PawSpital.Controllers;

public class ServiciiController : Controller
{
    private readonly IServiciuService _service;

    public ServiciiController(IServiciuService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _service.GetAllAsync();
        return View(list);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var serviciu = await _service.GetByIdAsync(id.Value);
        if (serviciu is null) return NotFound();

        return View(serviciu);
    }

    [RequireRole(AppRoles.Admin)]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Create([Bind("Nume,Pret,Descriere")] Serviciu model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var serviciu = await _service.GetByIdAsync(id.Value);
        if (serviciu is null) return NotFound();

        return View(serviciu);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Pret,Descriere")] Serviciu model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        var updated = await _service.UpdateAsync(model);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var serviciu = await _service.GetByIdAsync(id.Value);
        if (serviciu is null) return NotFound();

        return View(serviciu);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [RequireRole(AppRoles.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

