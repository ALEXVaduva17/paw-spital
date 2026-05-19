using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PawSpital.Models;
using PawSpital.Services;

namespace PawSpital.Controllers;

public class ProgramariController : Controller
{
    private readonly IProgramareService _programareService;
    private readonly IDoctorService _doctorService;
    private readonly IServiciuService _serviciuService;
    private readonly ISalaService _salaService;

    public ProgramariController(
        IProgramareService programareService,
        IDoctorService doctorService,
        IServiciuService serviciuService,
        ISalaService salaService)
    {
        _programareService = programareService;
        _doctorService = doctorService;
        _serviciuService = serviciuService;
        _salaService = salaService;
    }

    public async Task<IActionResult> Index()
    {
        var list = await _programareService.GetAllAsync();
        return View(list.OrderByDescending(p => p.Data).ToList());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var model = await _programareService.GetByIdAsync(id.Value);
        if (model is null) return NotFound();
        return View(model);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateSelectListsAsync();
        return View(new Programare { Data = DateTime.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NumePacient,Telefon,DoctorId,ServiciuId,SalaId,Data,Status")] Programare model)
    {
        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync(model.DoctorId, model.ServiciuId, model.SalaId);
            return View(model);
        }

        await _programareService.CreateAsync(model);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var model = await _programareService.GetByIdAsync(id.Value);
        if (model is null) return NotFound();

        await PopulateSelectListsAsync(model.DoctorId, model.ServiciuId, model.SalaId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,NumePacient,Telefon,DoctorId,ServiciuId,SalaId,Data,Status")] Programare model)
    {
        if (id != model.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateSelectListsAsync(model.DoctorId, model.ServiciuId, model.SalaId);
            return View(model);
        }

        var updated = await _programareService.UpdateAsync(model);
        if (!updated) return NotFound();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var model = await _programareService.GetByIdAsync(id.Value);
        if (model is null) return NotFound();

        return View(model);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _programareService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateSelectListsAsync(int? doctorId = null, int? serviciuId = null, int? salaId = null)
    {
        var doctori = await _doctorService.GetAllAsync();
        var servicii = await _serviciuService.GetAllAsync();
        var sali = await _salaService.GetAllAsync();

        ViewBag.DoctorId = new SelectList(doctori, "Id", "Nume", doctorId);
        ViewBag.ServiciuId = new SelectList(servicii, "Id", "Nume", serviciuId);
        ViewBag.SalaId = new SelectList(sali, "Id", "Nume", salaId);
    }
}

