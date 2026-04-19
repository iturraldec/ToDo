using Domain.Enums;

namespace Application.DTOs;

public record UserUpdateNameRequest(Guid Id, string Name);