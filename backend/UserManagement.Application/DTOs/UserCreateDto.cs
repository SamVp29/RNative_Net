using System.ComponentModel.DataAnnotations; // este espacio de nombres proporciona atributos que se pueden aplicar a las propiedades de una clase para definir reglas de validación y restricciones de datos, como [Required], [StringLength], [EmailAddress], entre otros. Estos atributos ayudan a garantizar que los datos ingresados cumplan con ciertos criterios antes de ser procesados o almacenados en la base de datos.  
namespace UserManagement.Application.DTOs; // DTOs (Data Transfer Objects) son objetos que se utilizan para transferir datos entre diferentes capas de una aplicación, como la capa de presentación y la capa de negocio. Los DTOs ayudan a encapsular los datos y a reducir el acoplamiento entre las capas, lo que facilita el mantenimiento y la evolución del código.

public class UserCreateDto
{
    [Required]
    [StringLength(100)] // este atributo indica que la propiedad Name es obligatoria y debe tener una longitud mínima de 2 caracteres y una longitud máxima de 100 caracteres. Esto ayuda a garantizar que los datos ingresados cumplan con ciertos criterios antes de ser procesados o almacenados en la base de datos.
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress] // este atributo indica que la propiedad Email es obligatoria y debe tener un formato de dirección de correo electrónico válido. Esto ayuda a garantizar que los datos ingresados cumplan con ciertos criterios antes de ser procesados o almacenados en la base de datos.
    public string Email { get; set; } = string.Empty;
     [Required]
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = "User";
    public bool Active { get; set; }
}
// El DTO UserCreateDto se utiliza para transferir los datos necesarios para crear un nuevo usuario en la aplicación. Contiene propiedades para el nombre, correo electrónico y estado de activación del usuario, que son esenciales para el proceso de creación de usuarios.