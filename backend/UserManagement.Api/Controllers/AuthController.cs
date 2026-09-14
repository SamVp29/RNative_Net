using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace UserManagement.Api.Controllers;

// Controlador encargado de manejar las operaciones de autenticación del sistema.
// Define la ruta base: /api/auth
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    // Servicio de negocio que encapsula la lógica de autenticación.
    private readonly IAuthService _authService;

    // Inyección de dependencias: se recibe el servicio autenticador desde DI.
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // Endpoint para iniciar sesión.
    // POST /api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        // Intenta autenticar al usuario con las credenciales recibidas.
        var token = await _authService.LoginAsync(dto);

        // Si el servicio responde nulo, significa que las credenciales son inválidas.
        if (token is null)
        {
            return Unauthorized(new
            {
                message = "Credenciales inválidas."
            });
        }

        // Si la autenticación fue correcta, devuelve el token generado.
        return Ok(new
        {
            token
        });
    }
}