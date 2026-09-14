using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Interfaces;

namespace UserManagement.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _repository;

    public AuthService(IUserRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Valida las credenciales del usuario y devuelve un token JWT si son correctas.
    /// </summary>
    /// <param name="dto">Datos de inicio de sesión enviados por el cliente.</param>
    /// <returns>Token JWT generado o null si las credenciales son inválidas.</returns>
    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var users = await _repository.GetAllAsync();

        var user = users.FirstOrDefault(
            u => u.Email == dto.Email
        );

        if (user is null)
        {
            return null;
        }

        var passwordValid = BCrypt.Net.BCrypt.Verify(
            dto.Password,
            user.PasswordHash
        );
        
        if (!passwordValid)
        {
            return null;
        }

        var claims = new[] //informacion que incluimos en el token
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                "clave-super-secreta-para-practica-123456789"
            )
        );

        // firma el jwt utilizando HMAC SHA-256
        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        // crea el jtw con su claims, duracion y las credenciales de la firma
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        //convertirlo a texto
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}