using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Assignments;

public class RegisterAssignmentUseCase
{
  private readonly IRepository<AssignmentEntity, AssignmentId> _repository;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterAssignmentUseCase(IRepository<AssignmentEntity, AssignmentId> repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task<string> Execute(RegisterAssignmentRequest request)
  {
    var entity = AssignmentEntity.Create(
                  AssignmentId.Create(), 
                  new UserId(request.AssignedToId),
                  new AssignmentTitle(request.Title),
                  new AssignmentDescription(request.Description),
                  DateTime.Now,
                  request.DueAt);

    await _repository.RegisterAsync(entity);
    await _unitOfWork.SaveChangesAsync();

    return entity.Id.ToString();
  }
}