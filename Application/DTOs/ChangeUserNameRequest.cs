using Domain.Enums;

namespace Application.DTOs;

public record ChangeUserNameRequest(Guid Id, string Name);