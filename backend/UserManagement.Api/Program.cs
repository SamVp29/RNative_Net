using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Data;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;

var builder = WebApplication.CreateBuilder(args);

/* vemos dependency injection, que es un patrón de diseño que permite a una clase
recibir sus dependencias desde el exterior en lugar de crearlas internamente. 
Esto facilita la prueba y el mantenimiento del código, ya que las dependencias pueden ser reemplazadas por implementaciones simuladas o alternativas según sea necesario. */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// dependencia injection usando scoped entregando una instancia de UserRepository cada vez que se solicita IUserRepository, lo que permite a los controladores y otros servicios interactuar con la capa de persistencia de datos sin preocuparse por los detalles de implementación.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

