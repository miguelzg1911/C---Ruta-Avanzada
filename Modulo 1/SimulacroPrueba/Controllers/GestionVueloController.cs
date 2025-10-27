using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulacroPrueba.Data;
using SimulacroPrueba.Models;

namespace SimulacroPrueba.Controllers;

public class GestionVueloController : Controller
{
    private readonly AppDbContext _context;

    public GestionVueloController(AppDbContext context)
    {
        _context = context;
    }

    // Listar Registros
    public async Task<IActionResult> Index()
    {
        var Gestion = await _context.GestionVuelos.ToListAsync();
        if (Gestion == null) return NotFound();
        return View(Gestion);
    } 
    
    // Crear
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(GestionVuelos gestionVuelos)
    {
        if (ModelState.IsValid)
        {
            ModelState.AddModelError(String.Empty,"Error, no puede estar vacio");
            return null;
        }

        try
        {
            _context.GestionVuelos.AddAsync(gestionVuelos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.Message);
            return null;
            throw;
        }
    }
    
    // Editar
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null) return NotFound();

        var gestionVuelos = await _context.GestionVuelos.FindAsync(id);
        if (gestionVuelos == null) return NotFound();
        return View(gestionVuelos);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, GestionVuelos gestionVuelos)
    {
        if (id != gestionVuelos.Id) return NotFound();

        if (ModelState.IsValid)
        {
            var existe = _context.GestionVuelos.FirstOrDefaultAsync(g => g.Id == gestionVuelos.Id);

            if (existe != null)
            {
                ModelState.AddModelError("id","Error, ya se registro.");
            }

            try
            {
                _context.Update(gestionVuelos);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pasajeros.Any(g => g.Id == gestionVuelos.Id))
                    return NotFound();
            }
        }
        return RedirectToAction(nameof(Index));
    }
    
    // Eliminar
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var gestion = await _context.GestionVuelos.FirstOrDefaultAsync(g => g.Id == id);
        if (gestion == null) return NotFound();
        return View(gestion);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var gestionVu = await _context.GestionVuelos.FindAsync(id);
        _context.GestionVuelos.Remove(gestionVu);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}