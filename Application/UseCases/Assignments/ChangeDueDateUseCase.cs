using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Assignments;
public class ChangeDueDateUseCase
{
  private readonly IAssignmentRepository _repository;
  private readonly IUnitOfWork _unitOfWork;
  public ChangeDueDateUseCase(IAssignmentRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }
  public async Task Execute(ChangeDueDateAssignmentRequest request)
  {
    var assignmentId = new AssignmentId(request.AssignmentId);
    var assignment = await _repository.GetByIdAsync(assignmentId);

    if (assignment is null) throw new Exception("La tarea no existe.");

    assignment.ChangeDueDate(new AssignmentDueAt(request.NewDueAt));

    await _repository.ChangeDueDateAsync(assignment, new AssignmentDueAt(request.NewDueAt));
    
    await _unitOfWork.SaveChangesAsync();
  }
}