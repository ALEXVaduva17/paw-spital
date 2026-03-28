using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawSpital.Data;
using PawSpital.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PawSpital.Controllers
{
    public class DepartamenteController : Controller
    {
        private readonly SpitalContext _context;

        public DepartamenteController(SpitalContext context)
        {
            _context = context;
        }

        // READ ALL (Get all records)
        public async Task<IActionResult> Index()
        {
            var list = await _context.Departamente.ToListAsync();
            return View(list);
        }

        // GET: CREATE (Form for new record)
        public IActionResult Create()
        {
            return View();
        }

        // POST: CREATE (Save to DB)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nume,Descriere")] Departament model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: UPDATE (Form to edit a record)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var departament = await _context.Departamente.FindAsync(id);
            if (departament == null) return NotFound();

            return View(departament);
        }

        // POST: UPDATE (Save changes to DB)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nume,Descriere")] Departament model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentExists(model.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: DELETE (Confirm deletion)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var departament = await _context.Departamente.FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null) return NotFound();

            return View(departament);
        }

        // POST: DELETE (Execute deletion from DB)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var departament = await _context.Departamente.FindAsync(id);
            if (departament != null)
            {
                _context.Departamente.Remove(departament);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentExists(int id)
        {
            return _context.Departamente.Any(e => e.Id == id);
        }
    }
}
