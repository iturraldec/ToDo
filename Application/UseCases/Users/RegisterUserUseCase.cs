#pragma warning disable IDE0005
using Application.DTOs;
using Domain.Exceptions;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;
public class RegisterUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  public RegisterUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;
  public async Task<Guid> Execute(UserRequest request)
  {
    var email = new UserEmail(request.Email);

    var existingUser = await _repository.GetByEmailAsync(email);
    
    if (existingUser != null)
    {
      throw new AlreadyExistsException($"El email {request.Email} ya está registrado.");
    }

    var id = UserId.FromGuid(Guid.NewGuid());
    
    var entity = new UserEntity(
                          id,
                          new UserName(request.Name),
                          email,
                          request.Role
                      );

    await _repository.AddAsync(entity);
    
    return id.Value;
  }
}