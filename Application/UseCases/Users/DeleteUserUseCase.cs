using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;

public class DeleteUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  
  public DeleteUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;
  
  public async Task Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(UserId.FromGuid(id));

    if (entity == null) throw new Exception("User not found");

    await _repository.DeleteAsync(UserId.FromGuid(id));
  }
}