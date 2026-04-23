using Domain.Enums;

namespace Application.DTOs;

public record RegisterAssignmentRequest(
  Guid AssignedToId,
  string Title, 
  string Description,
  DateTime DueAt
);