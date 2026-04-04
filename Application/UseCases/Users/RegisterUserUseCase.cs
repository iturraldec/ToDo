#pragma warning disable IDE0005
using Application.DTOs;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;
public class RegisterUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  public RegisterUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;
  public async Task Execute(UserRequest request)
  {
    // 1. Validar unicidad (Regla de aplicación)
    /* var userEmail = new UserEmail(email);
    var existing = await _repository.GetByEmailAsync(userEmail);
    if (existing != null) throw new Exception("El usuario ya existe.");
 */
    // 2. Generar Identidad y Entidad
    var user = new UserEntity(
                    UserId.CreateUnique(), 
                    new UserName(request.Name),
                    new UserEmail(request.Email),
                    request.Role
    );

    // 3. Persistir
    await _repository.AddAsync(user);
  }
}