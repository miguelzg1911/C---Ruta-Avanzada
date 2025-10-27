using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimulacroPrueba.Data;
using SimulacroPrueba.Models;

namespace SimulacroPrueba.Controllers;

public class PasajeroController : Controller
{
    private readonly AppDbContext _context;

    public PasajeroController(AppDbContext context)
    {
        _context = context;
    }

    // Listar Pasajeros
    public async Task<IActionResult> Index()
    {
        var pasajeros = await _context.Pasajeros.ToListAsync();
        if (pasajeros == null) return NotFound();
        return View(pasajeros);
    }
    
    // Crer Pasajeros
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(int id, Pasajeros pasajero)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(String.Empty, "Error, no puede estar vacio");
            return null;
        }
        if (await _context.Pasajeros.AnyAsync(p => p.Documento == pasajero.Documento && p.Id != id))
        {
            ModelState.AddModelError(nameof(pasajero.Documento), "Error, el documento es repetido. Intente con otro!.");
            return View(pasajero);
        }

        try
        {
            _context.Pasajeros.Add(pasajero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            return null;
            // throw;
        }
    }
    
    // Editar Pasajeros
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        if (id == null) return NotFound();
        
        var pasajero = await _context.Pasajeros.FindAsync(id);
        if (pasajero == null) return NotFound();
        return View(pasajero);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Pasajeros pasajero)
    {
        if (id != pasajero.Id) return BadRequest();
        if (!ModelState.IsValid) return View(pasajero);
        {
            if (await _context.Pasajeros.AnyAsync(p => p.Documento == pasajero.Documento && p.Id != id))
            {
                ModelState.AddModelError(nameof(pasajero.Documento),"Error, el documento es repetido. Intente con otro!!");    
                return View(pasajero);
            }
            
            try
            {
                _context.Update(pasajero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pasajeros.AnyAsync(p => p.Id == pasajero.Id))
                    return NotFound();
            }
        }
        return RedirectToAction(nameof(Index));
    }
    
    // Eliminar pasajero
    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        
        var pasajero = await _context.Pasajeros.FirstOrDefaultAsync(p => p.Id == id);
        if (pasajero == null) return NotFound();
        return View(pasajero);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var pasajero = await _context.Pasajeros.FindAsync(id);
        _context.Pasajeros.Remove(pasajero);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}