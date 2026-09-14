using UserManagement.Domain.Entities;
using UserManagement.Domain.Interfaces;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Services;

//implementa el contrato definido en IUserService, que representa las operaciones que nuestra aplicacion ofrece para interactuar con la entidad User, sin exponer los detalles de implementación de la capa de persistencia de datos.
public class UserService : IUserService
{
    // se usa IUserRepository porque depende del contrato esto es dependecy inversion principle
    private readonly IUserRepository _repository;

    //Constructor que recibe una instancia de IUserRepository a través de inyección de dependencias, lo que permite a la clase UserService interactuar con la capa de persistencia de datos sin preocuparse por los detalles de implementación.
    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(User user)
    {
        await _repository.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        await _repository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user is null)
        {
            return;
        }

        await _repository.DeleteAsync(user);
    }
}