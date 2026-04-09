using Application.DTOs;
using Domain.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.UseCases.Users;

public class GetAllUsersUseCase
{
  private readonly IUserRepository _repository;

  public GetAllUsersUseCase(IUserRepository repository) => _repository = repository;

  public async Task<IEnumerable<UserResponse>> Execute()
  {
    var users = await _repository.GetAllAsync();

    return users.Select(user => new UserResponse(user.Name.Value, user.Email.Value, (Roles) user.Role));
  }
}