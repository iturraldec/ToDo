using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;
public class GetDetailsByIdUserUseCase
{
  private readonly IUserReads _userReads;
  public GetDetailsByIdUserUseCase(IUserReads userReads) => _userReads = userReads;
  public async Task<UserResponse?> Execute(Guid id) => await _userReads.GetDetailsByIdAsync(id.ToString());
}