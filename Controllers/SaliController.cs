using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawSpital.Models;
using PawSpital.Services;

namespace PawSpital.Controllers;

public class SaliController : Controller
{
    private readonly ISalaService _service;

    public SaliController(ISalaService service)
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

        var sala = await _service.GetByIdAsync(id.Value);
        if (sala is null) return NotFound();

        return View(sala);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([Bind("Nume,Etaj")] Sala model)
    {
        if (!ModelState.IsValid)
            return View(model);

        await _service.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var sala = await _service.GetByIdAsync(id.Value);
        if (sala is null) return NotFound();

        return View(sala);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Etaj")] Sala model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid)
            return View(model);

        var updated = await _service.UpdateAsync(model);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var sala = await _service.GetByIdAsync(id.Value);
        if (sala is null) return NotFound();

        return View(sala);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

