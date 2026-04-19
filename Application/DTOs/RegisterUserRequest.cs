using Domain.Enums;

namespace Application.DTOs;

public record RegisterUserRequest(short Role, string Name, string Email);