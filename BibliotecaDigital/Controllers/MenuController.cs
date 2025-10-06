using BibliotecaDigital.Infrastucture;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaDigital.Controllers;

public class MenuController : Controller
{
    private readonly AppDbContext _context;

    public MenuController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View( );
    }
}