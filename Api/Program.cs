#pragma warning disable IDE0005
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.UseCases.Users;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Infrastructure.Models;
using Infrastructure.Implementations;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Entity Framework con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Registrar el Repositorio (Capa de Infraestructura)
builder.Services.AddScoped<IRepository<UserEntity, UserId>, UserRepository>();
builder.Services.AddScoped<RegisterUserUseCase>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Endpoint para probar la inserción del primer usuario
app.MapPost("/users", async (UserRequest request, RegisterUserUseCase useCase) =>
{
    await useCase.Execute(request);
    return Results.Ok(new { message = "Usuario registrado exitosamente" });
});

app.MapGet("/", () => "API ToDo Lista para pruebas").WithName("GetRoot");

app.Run();