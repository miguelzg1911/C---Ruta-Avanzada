using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaDigital.Infrastucture;
using BibliotecaDigital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BibliotecaDigital.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly AppDbContext _context;

        public PrestamosController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var prestamos = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .ToListAsync();
            
            return View(prestamos);
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestamo == null) return NotFound();

            return View(prestamo);
        }
        
        public IActionResult Create()
        {
            ViewBag.Usuarios = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Usuarios, "Id", "Nombre"
            );

            ViewBag.Libros = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
                _context.Libros, "Id", "Titulo"
            );
            return View();
        }
        
        [HttpPost]
                                                                                                                                                                                                                                                                                                                [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prestamos prestamo)
        {
            if (ModelState.IsValid)
            {
                // 🔹 Buscar el libro que se está prestando
                var libro = await _context.Libros.FindAsync(prestamo.IdLibro);
    
                if (libro == null)
                {
                    ModelState.AddModelError("", "El libro no existe.");
                }
                else if (libro.EjemplaresDisponibles <= 0)
                {
                    // 🔹 Validar que haya ejemplares disponibles
                    ModelState.AddModelError("", "No hay ejemplares disponibles de este libro.");
                }
                else
                {
                    // 🔹 Restar un ejemplar
                    libro.EjemplaresDisponibles -= 1;
    
                    // 🔹 Registrar préstamo con fecha de devolución a 7 días (puedes cambiarlo)
                    prestamo.FechaDevolucion = DateTime.Now;
    
                    _context.Add(prestamo);
                    await _context.SaveChangesAsync();
    
                    return RedirectToAction(nameof(Index));
                }
            }
    
            // 🔹 Recargar los selects en caso de error
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.IdUsuario);
            ViewBag.Libros = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
    
            return View(prestamo);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos.FindAsync(id);
            if (prestamo == null) return NotFound();
            
            ViewBag.Usuarios = new SelectList(_context.Usuarios, "Id", "Nombre", prestamo.IdUsuario);
            ViewBag.Libros = new SelectList(_context.Libros, "Id", "Titulo", prestamo.IdLibro);
            
            return View(prestamo);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prestamos prestamo)
        {
            if (id != prestamo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prestamo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Prestamos.Any(e => e.Id == prestamo.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prestamo);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var prestamo = await _context.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prestamo == null) return NotFound();

            return View(prestamo);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prestamo = await _context.Prestamos.FindAsync(id);
            _context.Prestamos.Remove(prestamo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Devolver(int id)
        {
            var prestamo = await _context.Prestamos
                .Include(p => p.Libro)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null) return NotFound();

            if (prestamo.FechaDevolucion == null)
            {
                prestamo.FechaDevolucion = DateTime.Now;
                prestamo.Libro.EjemplaresDisponibles += 1;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> PrestamosPorUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null) return NotFound();

            var prestamos = await _context.Prestamos
                .Include(p => p.Libro)
                .Where(p => p.IdUsuario == idUsuario)
                .ToListAsync();

            ViewBag.Usuario = usuario;
            return View(prestamos);
        }

        
        public async Task<IActionResult> PrestamosPorLibro(int idLibro)
        {
            var libro = await _context.Libros.FindAsync(idLibro);
            if (libro == null) return NotFound();

            var prestamos = await _context.Prestamos
                .Include(p => p.Usuario)
                .Where(p => p.IdLibro == idLibro)
                .ToListAsync();

            ViewBag.Libro = libro;
            return View(prestamos);
        }
    }
}
