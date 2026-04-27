namespace Application.DTOs;
public record ChangeDueDateAssignmentRequest(Guid AssignmentId, DateOnly NewDueAt);