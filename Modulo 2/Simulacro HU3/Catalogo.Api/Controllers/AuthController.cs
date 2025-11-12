using Catalogo.Application.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Registro de usuario
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] User user)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(user.Username) ||
                string.IsNullOrWhiteSpace(user.Email) ||
                string.IsNullOrWhiteSpace(user.PasswordHash) ||
                string.IsNullOrWhiteSpace(user.Document) ||
                string.IsNullOrWhiteSpace(user.Role.ToString()))
            {
                return BadRequest("Todos los campos son obligatorios.");
            }

            await _authService.RegisterAsync(
                user.Username,
                user.Email,
                user.PasswordHash,
                user.Document,
                user.Role.ToString()
            );

            return Ok(new { message = "Usuario registrado correctamente" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Login de usuario
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] User user)
    {
        try
        {
            var token = await _authService.LoginAsync(user.Email, user.PasswordHash);

            if (token == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}