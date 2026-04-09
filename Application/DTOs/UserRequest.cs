using Domain.Enums;

namespace Application.DTOs;

public record UserRequest(Guid? Id, string Name, string Email, Roles Role);