using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repositories;

/* Implementa el contrato de IUserRepository previamente definido, por lo que
esta obligado a implementar todos sus métodos */
/* se podria decir que cumple con polimorfismo, ya que podemos tener diferentes implementaciones de IUserRepository y el código que lo utiliza no necesita saber cuál implementación específica se está utilizando.
y abstracción, ya que define una interfaz que oculta los detalles de implementación y permite a los consumidores interactuar con el repositorio de usuarios sin preocuparse por cómo se realiza la persistencia de datos.
 */
public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context; // declaramos un campo privado readonly que representa el contexto de la base de datos y nos permite realizar operaciones CRUD en la entidad User a través del DbSet<User> Users.

    //esto es un constructor que actua como inyección de dependencias, que es un patrón de diseño que permite a una clase recibir sus dependencias desde el exterior en lugar de crearlas internamente.
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    // accesos a Sqlite de forma asincrónica, que es una forma de programación que permite realizar operaciones de manera no bloqueante, lo que significa que el hilo principal puede continuar ejecutando otras tareas mientras se espera la finalización de una operación asincrónica.
    // por eso usamos _context.Users por nuestro DbContext
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    /* tenemos dos operaciones diferentes, le decimos a EF Core quiero agregar un nuevo usuario pero todavia necesitamos guardar los cambios en la base de datos con el savechangesasync */
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /* tenemos dos operaciones diferentes, le decimos a EF Core quiero actualizar un usuario existente pero todavia necesitamos guardar los cambios en la base de datos con el savechangesasync */
    public async Task UpdateAsync(User user)
{
    var existingUser = await _context.Users.FindAsync(user.Id);

    if (existingUser is null)
    {
        return;
    }

    existingUser.Name = user.Name;
    existingUser.Email = user.Email;
    existingUser.Active = user.Active;

    await _context.SaveChangesAsync();
}
    /* tenemos dos operaciones diferentes, le decimos a EF Core quiero eliminar un usuario existente pero todavia necesitamos guardar los cambios en la base de datos con el savechangesasync */
    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}

//Polimorfismo podemos trabajar mediante la interfaz aunque la implementacion concreta sea UserRepository