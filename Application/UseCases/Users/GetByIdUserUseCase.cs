using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;

public class GetByIdUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;

  public GetByIdUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;

  public async Task<UserEntity?> Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(new UserId(id));
 
    if (entity == null) throw new Exception("User not found");

    return entity;
  }
}