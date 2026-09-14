using UserManagement.Application.DTOs;

namespace UserManagement.Application.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(LoginDto dto);
}