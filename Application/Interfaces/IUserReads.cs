using Domain.ValueObjects;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserReads
{
  Task<bool> IsEmailExistsAsync(string email);
  Task<IReadOnlyList<UserResponse>> GetAllUsersAsync();
  Task<UserResponse?> GetDetailsByIdAsync(string id);
}