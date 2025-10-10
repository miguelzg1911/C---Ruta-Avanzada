using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaDigital.Infrastucture;
using BibliotecaDigital.Models;

namespace BibliotecaDigital.Controllers
{
    public class LibrosController : Controller
    {
        private readonly AppDbContext _context;

        public LibrosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Libros
        public async Task<IActionResult> Index()
        {
            return View(await _context.Libros.ToListAsync());
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Libros libro)
        {
            if (ModelState.IsValid)
            {
                var existingLibro = await _context.Libros
                    .FirstOrDefaultAsync(l => l.Codigo == libro.Codigo);

                if (existingLibro != null)
                {
                    // Si el libro ya existe, agrega un error al ModelState
                    ModelState.AddModelError("Codigo", "Ya existe un libro con este código.");
                }
                else
                {
                    _context.Add(libro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); 
                }
            }
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null) return NotFound();
            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Libros libro)
        {
            if (id != libro.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingLibro = await _context.Libros
                    .FirstOrDefaultAsync(l => l.Codigo == libro.Codigo && l.Id != id);

                if (existingLibro != null)
                {
                    ModelState.AddModelError("Codigo", "Ya existe otro libro con este código.");
                    return View(libro);
                }
                
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Libros.Any(e => e.Id == libro.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var libro = await _context.Libros.FirstOrDefaultAsync(m => m.Id == id);
            if (libro == null) return NotFound();

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
