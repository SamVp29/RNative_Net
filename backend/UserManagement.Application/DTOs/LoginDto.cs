using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
// Este DTO representa lo que el cliente úede enviar el Login
