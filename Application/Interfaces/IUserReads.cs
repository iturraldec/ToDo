using Domain.ValueObjects;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserReads
{
  Task<bool> IsIdExistsAsync(UserId id);
  Task<bool> IsEmailExistsAsync(string email);
  Task<IReadOnlyList<UserResponse>> GetAllUsersAsync();
  Task<UserResponse?> GetDetailsByIdAsync(Guid id);
}