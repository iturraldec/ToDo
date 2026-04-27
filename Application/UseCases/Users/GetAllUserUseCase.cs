using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCases.Users;
public class GetAllUsersUseCase
{
  private readonly IUserReads _repository;
  public GetAllUsersUseCase(IUserReads repository) => _repository = repository;
  public async Task<IReadOnlyList<UserResponse>> Execute() => await _repository.GetAllUsersAsync();
}