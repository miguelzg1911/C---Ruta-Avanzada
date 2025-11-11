using Catalogo.Domain.Models;

namespace Catalogo.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string email, string password);
    Task<bool> RegisterAsync(string username, string email, string password, string role);
}