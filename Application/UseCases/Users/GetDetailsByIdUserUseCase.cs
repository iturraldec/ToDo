using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Application.UseCases.Users;
public class GetDetailsByIdUserUseCase
{
  private readonly IUserReads _userReads;
  public GetDetailsByIdUserUseCase(IUserReads userReads) => _userReads = userReads;
  public async Task<UserResponse?> Execute(Guid id)
  {
    var result = await _userReads.GetDetailsByIdAsync(id);

    if(result == null) throw new NotFoundException("User not found");

    return result;
  }
}