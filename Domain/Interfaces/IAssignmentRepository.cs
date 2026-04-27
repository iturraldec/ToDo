using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Interfaces;
public interface IAssignmentRepository : IRepository<AssignmentEntity, AssignmentId>
{
  Task ChangeStatusAsync(AssignmentEntity assignment, AssignmentStatusEnum newStatus, UserRolesEnum userRole);
  Task ChangeDueDateAsync(AssignmentEntity assignment, AssignmentDueAt newDueAt);
}