using Application.DTOs;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;

public class UpdateUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;

  public UpdateUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;

  public async Task Execute(UserRequest request)
  {
    var userId = UserId.FromGuid(request.Id.Value);
    var entity = await _repository.GetByIdAsync(userId);
    
    entity.Update(new UserName(request.Name), UserEmail.Create(request.Email), (Roles) request.Role);

    await _repository.UpdateAsync(entity);
  }
}