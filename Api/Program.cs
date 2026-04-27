using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Users;
using Application.UseCases.Assignments;
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

// 2. Registrar las implementaciones
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<AssignmentRepository>();
builder.Services.AddScoped<UnitOfWork>();

// Registra la interfaz con su implementación concreta
builder.Services.AddScoped<IUserRepository>(sp => sp.GetRequiredService<UserRepository>());
builder.Services.AddScoped<IUserReads>(sp => sp.GetRequiredService<UserRepository>());

builder.Services.AddScoped<IAssignmentRepository>(sp => sp.GetRequiredService<AssignmentRepository>());
builder.Services.AddScoped<IAssignmentReads>(sp => sp.GetRequiredService<AssignmentRepository>());

builder.Services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UnitOfWork>());

// casos de uso
builder.Services.AddScoped<RegisterUserUseCase>();
builder.Services.AddScoped<GetByIdUserUseCase>();
builder.Services.AddScoped<GetDetailsByIdUserUseCase>();
builder.Services.AddScoped<GetAllUsersUseCase>();
builder.Services.AddScoped<ChangeNameUserUseCase>();
builder.Services.AddScoped<DeleteUserUseCase>();

builder.Services.AddScoped<RegisterAssignmentUseCase>();
builder.Services.AddScoped<GetDetailsByIdAssignmentUseCase>();
builder.Services.AddScoped<GetAllAssignmentsUseCase>();
builder.Services.AddScoped<GetByUserIdAssignmentsUseCase>();

// arrancamos
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

////////////////////////////////////////////// User - ToDo List API //////////////////////////////////////////////
// registrar un nuevo usuario
app.MapPost("/users", async (RegisterUserRequest request, RegisterUserUseCase useCase) =>
{
  var id = await useCase.Execute(request);
  
  return Results.Created($"/users/{id}", new { Id = id });
});

// traer un usuario por su id
app.MapGet("/users/{id}/details", async (Guid id, GetDetailsByIdUserUseCase useCase) => 
{
    var user = await useCase.Execute(id);
    
    return Results.Ok(user);
});

// listado de usuarios
app.MapGet("/users", async (GetAllUsersUseCase useCase) =>
{
    var users = await useCase.Execute();
    return Results.Ok(users);
});

// actualizar el nombre de un usuario
app.MapPut("/users/update-name", async (ChangeUserNameRequest request, ChangeNameUserUseCase useCase) =>
{
    await useCase.Execute(request);
    
    return Results.NoContent();
});

// eliminar un usuario por su id
app.MapDelete("/users/{id}", async (Guid id, DeleteUserUseCase useCase) =>
{
    await useCase.Execute(id);
    
    return Results.NoContent();
});

////////////////////////////////////////////// Assignment - ToDo List API //////////////////////////////////////////////

// agregar tarea
app.MapPost("/assignments", async (RegisterAssignmentRequest request, RegisterAssignmentUseCase useCase) =>
{
  var id = await useCase.Execute(request);

  return Results.Created($"/assignments/{id}", new { Id = id });
});

// traer una tarea por su id
app.MapGet("/assignments/{id}/details", async (Guid id, GetDetailsByIdAssignmentUseCase useCase) => 
{
    var assignment = await useCase.Execute(id);
    
    return Results.Ok(assignment);
});

// listado de tareas
app.MapGet("/assignments", async (GetAllAssignmentsUseCase useCase) =>
{
    var assignments = await useCase.Execute();

    return Results.Ok(assignments);
});


// listado de tareas por usuario
app.MapGet("/assignments/user/{userId}", async (Guid userId, GetByUserIdAssignmentsUseCase useCase) =>
{
    var assignments = await useCase.Execute(userId);

    return Results.Ok(assignments);
});

//
app.MapGet("/", () => "API ToDo Lista para pruebas").WithName("GetRoot");

app.Run();

// dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=ToDo;Username=postgres;Password=J1z01234_" Npgsql.EntityFrameworkCore.PostgreSQL -f -o Models