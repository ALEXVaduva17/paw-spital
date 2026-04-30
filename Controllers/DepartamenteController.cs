using Microsoft.AspNetCore.Mvc;
using PawSpital.Models;
using PawSpital.Security;
using PawSpital.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PawSpital.Controllers
{
    public class DepartamenteController : Controller
    {
        private readonly IDepartamentService _service;

        public DepartamenteController(IDepartamentService service)
        {
            _service = service;
        }

        // READ ALL (Get all records)
        public async Task<IActionResult> Index()
        {
            var list = await _service.GetAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var departament = await _service.GetByIdAsync(id.Value);
            if (departament == null) return NotFound();

            return View(departament);
        }

        // GET: CREATE (Form for new record)
        [RequireRole(AppRoles.Admin)]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CREATE (Save to DB)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole(AppRoles.Admin)]
        public async Task<IActionResult> Create([Bind("Nume,Descriere")] Departament model)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: UPDATE (Form to edit a record)
        [RequireRole(AppRoles.Admin)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departament = await _service.GetByIdAsync(id.Value);
            if (departament == null) return NotFound();

            return View(departament);
        }

        // POST: UPDATE (Save changes to DB)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequireRole(AppRoles.Admin)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] Departament model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var updated = await _service.UpdateAsync(model);
                if (!updated) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DELETE (Confirm deletion)
        [RequireRole(AppRoles.Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departament = await _service.GetByIdAsync(id.Value);
            if (departament == null) return NotFound();

            return View(departament);
        }

        // POST: DELETE (Execute deletion from DB)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RequireRole(AppRoles.Admin)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
