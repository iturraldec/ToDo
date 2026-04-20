using Domain.Enums;

namespace Application.DTOs;

public record UserResponse(string Role, string Name, string Email, string Status, DateTime CreatedAt, DateTime? UpdatedAt);