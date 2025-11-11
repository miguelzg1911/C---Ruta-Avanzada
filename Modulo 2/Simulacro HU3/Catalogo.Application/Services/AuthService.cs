using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Catalogo.Application.Interfaces;
using Catalogo.Domain.Interfaces;
using Catalogo.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Catalogo.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // Login
    
    public async Task<string?> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null) return null;

        // Verificar hash
        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!passwordValid) return null;

        // Crear claims del token
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Registro
    
    public async Task<bool> RegisterAsync(string username, string email, string password, string role)
    {
        var existing = await _userRepository.GetUserByEmailAsync(email);
        if (existing != null)
            throw new Exception("El correo ya está registrado");

        var newUser = new User
        {
            Username = username,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = Enum.TryParse<User.UserRole>(role, out var parsedRole) ? parsedRole : User.UserRole.User
        };

        await _userRepository.AddAsync(newUser);
        await _userRepository.SaveChangesAsync();

        return true;
    }
}
