using Application.DTOs;
using Application.Interfaces;

namespace Application.UseCases.Assignments;
public class GetAllAssignmentsUseCase
{
  private readonly IAssignmentReads _repository;
  public GetAllAssignmentsUseCase(IAssignmentReads repository) => _repository = repository;
  public async Task<IReadOnlyList<AssignmentResponse>> Execute() => await _repository.GetAllAssignmentsAsync();
}