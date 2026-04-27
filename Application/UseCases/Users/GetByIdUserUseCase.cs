using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.UseCases.Users;

public class GetByIdUserUseCase
{
  private readonly IUserRepository _repository;
  public GetByIdUserUseCase(IUserRepository repository) => _repository = repository;
  public async Task<UserEntity?> Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(new UserId(id));
 
    if (entity == null) throw new NotFoundException("User not found");

    return entity;
  }
}