namespace Application.DTOs;
public record RegisterAssignmentRequest(
  Guid AssignedToId,
  string Title, 
  string Description,
  DateOnly DueAt
);