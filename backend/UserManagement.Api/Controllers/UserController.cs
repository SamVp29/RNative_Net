using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;  
using UserManagement.Application.DTOs; // DTOs (Data Transfer Objects) son objetos que se utilizan para transferir datos entre diferentes capas de una aplicación, como la capa de presentación y la capa de negocio. Los DTOs ayudan a encapsular los datos y a reducir el acoplamiento entre las capas, lo que facilita el mantenimiento y la evolución del código.
using Microsoft.AspNetCore.Authorization;

namespace UserManagement.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]

public class UsersController : ControllerBase // esta clase es un controlador de API que maneja las solicitudes HTTP relacionadas con la entidad User y hereda de ControllerBase, que es una clase base proporcionada por ASP.NET Core para crear controladores de API.
{
    private readonly IUserService _service; // declaramos un campo privado readonly que representa el servicio de usuario y nos permite realizar operaciones CRUD en la entidad User a través de los métodos definidos en la interfaz IUserService.
    public UsersController(IUserService service) //esto es un constructor que actua como inyección de dependencias, que es un patrón de diseño que permite a una clase recibir sus dependencias desde el exterior en lugar de crearlas internamente.
    {
        _service = service;
    }
    [HttpGet] // esto es un atributo que indica que el método GetAllAsync es un endpoint HTTP GET, lo que significa que se puede acceder a él mediante una solicitud GET a la URL api/user.
    public async Task<ActionResult<List<User>>> GetAll() // este método es un endpoint
    {
        var users = await _service.GetAllAsync(); // llamamos al método GetAllAsync del servicio de usuario para obtener todos los usuarios de la base de datos de forma asincrónica.
        return Ok(users); // devolvemos una respuesta HTTP 200 OK con la lista de usuarios en el cuerpo de la respuesta.
    }

    [HttpPost] // esto es un atributo que indica que el método CreateAsync es un endpoint HTTP POST, lo que significa que se puede acceder a él mediante una solicitud POST a la URL api/user.
    public async Task<ActionResult<User>> Create(UserCreateDto dto) // este método es un endpoint que recibe un objeto UserCreateDto en el cuerpo de la solicitud y lo agrega a la base de datos.
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Active = dto.Active,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        await _service.AddAsync(user); // llamamos al método AddAsync del servicio de usuario para agregar el usuario a la base de datos de forma asincrónica.
       /*  return Ok(user); // devolvemos una respuesta HTTP 200 OK con el usuario agregado en el cuerpo de la respuesta. */
        return CreatedAtAction(
        nameof(GetById),
        new { id = user.Id },
        user
    );
    }

    [HttpGet("{id}")] // esto es un atributo que indica que el método GetByIdAsync es un endpoint HTTP GET con un parámetro de ruta id, lo que significa que se puede acceder a él mediante una solicitud GET a la URL api/user/{id}.
    public async Task<ActionResult<User>> GetById(int id) // este método es un endpoint que recibe un parámetro id en la ruta de la solicitud y busca un usuario con ese id en la base de datos.
    {
        var user = await _service.GetByIdAsync(id); // llamamos al método GetByIdAsync del servicio de usuario para obtener el usuario con el id especificado de forma asincrónica.
        if (user is null) // si no se encuentra ningún usuario con ese id, devolvemos una respuesta HTTP 404 Not Found.
        {
            return NotFound();
        }
        return Ok(user); // si se encuentra el usuario, devolvemos una respuesta HTTP 200 OK con el usuario en el cuerpo de la respuesta.
    }

    [HttpPut("{id}")] // esto es un atributo que indica que el método UpdateAsync es un endpoint HTTP PUT con un parámetro de ruta id, lo que significa que se puede acceder a él mediante una solicitud PUT a la URL api/user/{id}.
    public async Task<ActionResult<User>> Update(int id, UserUpdateDto dto) // este método es un endpoint que recibe un parámetro id en la ruta de la solicitud y un objeto User en el cuerpo de la solicitud, y actualiza el usuario con ese id en la base de datos.
    {
       var existingUser = await _service.GetByIdAsync(id);
        if (existingUser is null) // si no se encuentra ningún usuario con ese id, devolvemos una respuesta HTTP 404 Not Found.
        {
            return NotFound();
        }
        
        existingUser.Name = dto.Name; // actualizamos las propiedades del usuario existente con los valores del objeto User recibido en la solicitud.
        existingUser.Email = dto.Email;
        existingUser.Active = dto.Active;

        await _service.UpdateAsync(existingUser); // llamamos al método UpdateAsync del servicio de usuario para actualizar el usuario en la base de datos de forma asincrónica.
        return Ok(existingUser); // devolvemos una respuesta HTTP 200 OK con el usuario actualizado en el cuerpo de la respuesta.
    }

    [HttpDelete("{id}")] // esto es un atributo que indica que el método DeleteAsync es un endpoint HTTP DELETE con un parámetro de ruta id, lo que significa que se puede acceder a él mediante una solicitud DELETE a la URL api/user/{id}.   
    public async Task<IActionResult> Delete(int id) // este método es un endpoint que recibe un parámetro id en la ruta de la solicitud y elimina el usuario con ese id de la base de datos.
    {
        var user = await _service.GetByIdAsync(id); // llamamos al método GetByIdAsync del servicio de usuario para obtener el usuario con el id especificado de forma asincrónica.
        if (user is null) // si no se encuentra ningún usuario con ese id, devolvemos una respuesta HTTP 404 Not Found.
        {
            return NotFound();
        }
        await _service.DeleteAsync(id); // llamamos al método DeleteAsync del servicio de usuario para eliminar el usuario de la base de datos de forma asincrónica.
        return NoContent(); // devolvemos una respuesta HTTP 204 No Content para indicar que la
    }
}

// controllers antes del uso de DTOs
/*     [HttpPost] // esto es un atributo que indica que el método CreateAsync es un endpoint HTTP POST, lo que significa que se puede acceder a él mediante una solicitud POST a la URL api/user.
    public async Task<ActionResult<User>> Create(User user) // este método es un endpoint que recibe un objeto User en el cuerpo de la solicitud y lo agrega a la base de datos.
    {
        await _service.AddAsync(user); // llamamos al método AddAsync del servicio de usuario para agregar el nuevo usuario a la base de datos de forma asincrónica.
        return Ok(user); // devolvemos una respuesta HTTP 200 OK con el usuario recién creado en el cuerpo de la respuesta.
    } */


/*     [HttpPut("{id}")] // esto es un atributo que indica que el método UpdateAsync es un endpoint HTTP PUT con un parámetro de ruta id, lo que significa que se puede acceder a él mediante una solicitud PUT a la URL api/user/{id}.
    public async Task<ActionResult<User>> Update(int id, User user) // este método es un endpoint que recibe un parámetro id en la ruta de la solicitud y un objeto User en el cuerpo de la solicitud, y actualiza el usuario con ese id en la base de datos.
    {
        var existingUser = await _service.GetByIdAsync(id); // llamamos al método GetByIdAsync del servicio de usuario para obtener el usuario con el id especificado de forma asincrónica.
        if (existingUser is null) // si no se encuentra ningún usuario con ese id, devolvemos una respuesta HTTP 404 Not Found.
        {
            return NotFound();
        }
        
        user.Id = id; // asignamos el id del usuario existente al objeto User recibido en la solicitud para asegurarnos de que estamos actualizando el usuario correcto.
        await _service.UpdateAsync(user); // llamamos al método UpdateAsync del servicio de usuario para actualizar el usuario en la base de datos de forma asincrónica.
        return Ok(user); // devolvemos una respuesta HTTP 200 OK con el usuario actualizado en
    } */