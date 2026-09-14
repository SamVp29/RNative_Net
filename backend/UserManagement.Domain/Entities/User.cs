/* User.cs en domain representa una entidad de nuestro negocio
no necesita saber que es http, controlles, react native, sql, entity framework etc. 
solo representa la entidad de nuestro negocio, en este caso un usuario */

//namespace sirve para organizar y agrupar clases
namespace UserManagement.Domain.Entities;

//definimos una clase llamada User
public class User
{
    //propiedades de la clase User
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; //inicializamos la propiedad Name con un valor por defecto de cadena vacía
    public string Email { get; set; } = string.Empty;
    public bool Active { get; set; }

    public string PasswordHash { get; set; } = string.Empty;
}