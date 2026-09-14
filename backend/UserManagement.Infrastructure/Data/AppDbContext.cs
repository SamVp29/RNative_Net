using Microsoft.EntityFrameworkCore; //Nos permite trabajar con Entity Framework Core, que es un ORM (Object-Relational Mapper) para interactuar con bases de datos relacionales en .NET.
// ademas de usar dbcontext, dbset, entity framework core, etc.
using UserManagement.Domain.Entities; //Nos permite acceder a la clase User definida en el proyecto UserManagement.Domain, que representa la entidad de usuario en nuestro dominio de negocio.

namespace UserManagement.Infrastructure.Data;

// aqui aparece algo de la clase AppDbContext, que hereda de DbContext, que es una clase base proporcionada por Entity Framework Core para interactuar con la base de datos.
// nuestro AppDbContext representa el contexto de la base de datos y nos permite realizar operaciones CRUD en la entidad User a través del DbSet<User> Users.
public class AppDbContext : DbContext
{
    // constructor de la clase AppDbContext que recibe opciones de configuración para el contexto de la base de datos y las pasa a la clase base DbContext.
    public AppDbContext(DbContextOptions<AppDbContext> options) 
    : base(options)
    {
    }

    // representa el conjunto de entidades User en la base de datos y nos permite realizar operaciones CRUD en la tabla correspondiente a la entidad User.
    public DbSet<User> Users { get; set; }
}