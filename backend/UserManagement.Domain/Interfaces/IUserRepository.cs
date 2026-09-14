/* Definimos la interfaz que representa un contrato para la implementación de un repositorio de usuarios 
Nos dice QUE HACER, pero no el COMO HACERLO*/

// usamos entidades de domínio
using UserManagement.Domain.Entities;

// organizamos y agrupamos clases relacionadas con la interfaz de repositorio de usuario
namespace UserManagement.Domain.Interfaces;

// definimos una interfaz llamada IUserRepository que representa un contrato para la implementación de un repositorio de usuarios
public interface IUserRepository
{
    // definimos métodos asincrónicos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en la entidad User
    Task<List<User>> GetAllAsync(); // Obtiene todos los usuarios
    Task<User?> GetByIdAsync(int id); // Obtiene un usuario por su ID
    Task AddAsync(User user); // Agrega un nuevo usuario
    Task UpdateAsync(User user); // Actualiza un usuario existente
    Task DeleteAsync(User user); // Elimina un usuario

}
//se podria decir que en IUserRepository usa abstraccion ya que define operaciones que se pueden realizar en un repositorio de usuarios sin exponer los detalles de implementación.