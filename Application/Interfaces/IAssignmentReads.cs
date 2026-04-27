using Domain.ValueObjects;
using Application.DTOs;

namespace Application.Interfaces;
public interface IAssignmentReads
{
  Task<AssignmentResponse?> GetDetailsByIdAsync(Guid id);
  Task<IReadOnlyList<AssignmentResponse>> GetAllAssignmentsAsync();
  Task<IReadOnlyList<AssignmentResponse>> GetByUserIdAssignmentsAsync(Guid userId);
}