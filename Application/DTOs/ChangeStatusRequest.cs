using Domain.Enums;
public record ChangeStatusRequest(AssignmentStatusEnum newStatus, UserRolesEnum userRole);