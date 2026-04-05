using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.UseCases.Users;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Infrastructure.Models;
using Infrastructure.Implementations;
using Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Entity Framework con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Registrar los Repositorios
builder.Services.AddScoped<IRepository<UserEntity, UserId>, UserRepository>();
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<GetByIdUserUseCase>();
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<UpdateUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Endpoint para probar la inserción del primer usuario
app.MapPost("/users", async (UserRequest request, RegisterUserUseCase useCase) =>
{
  var id = await useCase.Execute(request);
  
  return Results.Created($"/users/{id}", new { Id = id });
});

// El endpoint GET que servirá para esa URL
app.MapGet("/users/{id}", async (Guid id, GetByIdUserUseCase useCase) => 
{
    var user = await useCase.Execute(id);
    
    return Results.Ok(user);
});

//
app.MapGet("/users", async (GetAllUsersUseCase useCase) =>
{
    var users = await useCase.Execute();
    return Results.Ok(users);
});

// El endpoint PUT que servirá para actualizar un usuario
app.MapPut("/users", async (UserRequest request, UpdateUserUseCase useCase) =>
{
    await useCase.Execute(request);
    
    return Results.NoContent();
});

//
app.MapDelete("/users/{id}", async (Guid id, DeleteUserUseCase useCase) =>
{
    await useCase.Execute(id);
    
    return Results.NoContent();
});

//
app.MapGet("/", () => "API ToDo Lista para pruebas").WithName("GetRoot");

app.Run();