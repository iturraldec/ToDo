using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCases.Assignments;
public class GetByUserIdAssignmentsUseCase
{
  private readonly IAssignmentReads _repository;
  public GetByUserIdAssignmentsUseCase(IAssignmentReads repository) => _repository = repository;
  public async Task<IReadOnlyList<AssignmentResponse>> Execute(Guid userId) => await _repository.GetByUserIdAssignmentsAsync(userId);
}