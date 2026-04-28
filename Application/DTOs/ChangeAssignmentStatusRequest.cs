using Domain.Enums;
public record ChangeAssignmentStatusRequest(Guid UserId, Guid AssignmentId, short newStatus);