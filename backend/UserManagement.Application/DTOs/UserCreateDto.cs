namespace UserManagement.Application.DTOs; // DTOs (Data Transfer Objects) son objetos que se utilizan para transferir datos entre diferentes capas de una aplicación, como la capa de presentación y la capa de negocio. Los DTOs ayudan a encapsular los datos y a reducir el acoplamiento entre las capas, lo que facilita el mantenimiento y la evolución del código.

public class UserCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }
}
// El DTO UserCreateDto se utiliza para transferir los datos necesarios para crear un nuevo usuario en la aplicación. Contiene propiedades para el nombre, correo electrónico y estado de activación del usuario, que son esenciales para el proceso de creación de usuarios.