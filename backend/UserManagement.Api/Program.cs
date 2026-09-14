using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Data;
using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;
using System.Runtime.ExceptionServices;
using UserManagement.Api.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

/* vemos dependency injection, que es un patrón de diseño que permite a una clase
recibir sus dependencias desde el exterior en lugar de crearlas internamente. 
Esto facilita la prueba y el mantenimiento del código, ya que las dependencias pueden ser reemplazadas por implementaciones simuladas o alternativas según sea necesario. */
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=users.db"));

// dependencia injection usando scoped entregando una instancia de UserRepository cada vez que se solicita IUserRepository, lo que permite a los controladores y otros servicios interactuar con la capa de persistencia de datos sin preocuparse por los detalles de implementación.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    "clave-super-secreta-para-practica-123456789"
                )
            )
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>(); // Uso de Middleware

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

