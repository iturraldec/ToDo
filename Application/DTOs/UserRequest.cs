#pragma warning disable IDE0005
using Domain.Enums;

namespace Application.DTOs;
public record UserRequest(string Name, string Email, Roles Role);