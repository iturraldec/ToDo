using Domain.Enums;

namespace Application.DTOs;

public record AssignmentUpdateRequest(
  Guid id, 
  string Title, 
  string Description, 
  AssignmentStatus Status, 
  Guid AssignedToUserId
);