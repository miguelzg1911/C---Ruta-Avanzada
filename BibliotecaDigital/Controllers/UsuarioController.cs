using System;
using BibliotecaDigital.Infrastucture;
using BibliotecaDigital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaDigital.Controllers;

public class UsuarioController : Controller
{
    private readonly AppDbContext _context;

    public UsuarioController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        if (usuarios == null) return NotFound();
        return View(usuarios);
    }

    public async Task<IActionResult> Details(int Id)
    {
        var usuario = await _context.Usuarios.
            Include(u => u.prestamos)
            .ThenInclude(p => p.Libro)
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (usuario == null) return NotFound();
        return View(usuario);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Usuarios usuario)
    {
        if (!ModelState.IsValid) return View(usuario);

        if (await _context.Usuarios.AnyAsync(u => u.Documento == usuario.Documento))
        {
            ModelState.AddModelError(nameof(usuario.Documento), "El documento ya esta registrado.");
            return View(usuario);
        }

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Usuarios input)
    {
        if (id != input.Id) return BadRequest();
        if (!ModelState.IsValid) return View(input);
        
        if (await _context.Usuarios.AnyAsync(u => u.Documento == input.Documento && u.Id != id))
        {
            ModelState.AddModelError(nameof(input.Documento), "El documento ya está en uso por otro usuario.");
            return View(input);
        }

        try
        {
            _context.Update(input);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Usuarios.AnyAsync(u => u.Id == id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();

        return View(usuario); 
    }

    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario == null) return NotFound();
        
        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}


 