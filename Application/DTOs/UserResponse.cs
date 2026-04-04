using Domain.Enums;

namespace Application.DTOs;
public record UserResponse(string Name, string Email, Roles Role);