using Application.DTOs;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;
public class GetByIdUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  public GetByIdUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;
  public async Task<UserResponse?> Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(UserId.FromGuid(id));
 
    if (entity == null) return null;

    return new UserResponse(
                entity.Name.Value,
                entity.Email.Value,
                (Roles) entity.Role
              );
  }
}