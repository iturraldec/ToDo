using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.UseCases.Assignments;

public class GetByIdAssignmentUseCase
{
  private readonly IAssignmentRepository _repository;
  public GetByIdAssignmentUseCase(IAssignmentRepository repository) => _repository = repository;
  public async Task<AssignmentEntity?> Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(new AssignmentId(id));
 
    if (entity == null) throw new NotFoundException("Assignment not found");

    return entity;
  }
}