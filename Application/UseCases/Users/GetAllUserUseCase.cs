using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.UseCases.Users;

public class GetAllUsersUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;

  public GetAllUsersUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;

  public async Task<IEnumerable<UserResponse>> Execute()
  {
    var users = await _repository.GetAllAsync();

    return users.Select(user => new UserResponse(user.Name.Value, user.Email.Value, (Roles) user.Role));
  }
}