using System.ComponentModel.DataAnnotations;

namespace UserManagement.Application.DTOs;

public class UserUpdateDto
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool Active { get; set; }
}
// El DTO UserUpdateDto se utiliza para transferir los datos necesarios para actualizar un usuario existente en la aplicación. Contiene propiedades para el nombre, correo electrónico y estado de activación del usuario, que son esenciales para el proceso de actualización de usuarios.