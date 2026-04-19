using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.UseCases.Users;
//using Application.UseCases.Assignments;
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
builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRepository<AssignmentEntity, Guid>, AssignmentRepository>();
builder.Services.AddScoped<AddUserUseCase>();
builder.Services.AddScoped<GetByIdUserUseCase>();
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<UpdateNameUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();

//builder.Services.AddScoped<AddAssignmentUseCase>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Endpoint para probar la inserción del primer usuario
app.MapPost("/users", async (UserAddRequest request, AddUserUseCase useCase) =>
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

// actualizar el nombre de un usuario
app.MapPut("/users/update-name", async (UserUpdateNameRequest request, UpdateNameUserUseCase useCase) =>
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

////////////////////////////////////////////// Assignment - ToDo List API //////////////////////////////////////////////

/* app.MapPost("/assignments", async (AssignmentAddRequest request, AddAssignmentUseCase useCase) =>
{
  var id = await useCase.Execute(request);

  return Results.Created($"/assignments/{id}", new { Id = id });
});
 */
//
app.MapGet("/", () => "API ToDo Lista para pruebas").WithName("GetRoot");

app.Run();

// dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=ToDo;Username=postgres;Password=J1z01234_" Npgsql.EntityFrameworkCore.PostgreSQL -o Models