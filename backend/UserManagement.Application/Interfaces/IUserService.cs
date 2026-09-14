using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}
// representa las operaciones que nuestra aplicacion ofrece para interactuar con la entidad User, sin exponer los detalles de implementación de la capa de persistencia de datos.