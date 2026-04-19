using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.UseCases.Assignments;

public class AddAssignmentUseCase
{
  private readonly IRepository<AssignmentEntity, Guid> _repository;

  public AddAssignmentUseCase(IRepository<AssignmentEntity, Guid> repository) => _repository = repository;

  public async Task<Guid> Execute(AssignmentAddRequest request)
  {
    var assignment = new AssignmentEntity
    {
      Id = Guid.NewGuid(),
      Title = request.Title,
      Description = request.Description,
      Status = AssignmentStatus.Pending,
      CreatedAt = DateTime.Now,
      AssignedById = request.AssignedByUserId,
      AssignedToId = request.AssignedToUserId
    };
    
    await _repository.AddAsync(assignment);
    
    return assignment.Id;
  }
}