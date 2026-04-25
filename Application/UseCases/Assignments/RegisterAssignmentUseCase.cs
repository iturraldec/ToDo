using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Assignments;

public class RegisterAssignmentUseCase
{
  private readonly IRepository<AssignmentEntity, AssignmentId> _repository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserReads _userReadService;

  public RegisterAssignmentUseCase(IRepository<AssignmentEntity, AssignmentId> repository, IUnitOfWork unitOfWork, IUserReads userReadService)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
    _userReadService = userReadService;
  }

  public async Task<string> Execute(RegisterAssignmentRequest request)
  {
    var userId = new UserId(request.AssignedToId);
    var createAt = AssignmentCreadtedAt.Create();

    if (! await _userReadService.IsIdExistsAsync(userId)) throw new NotFoundException($"El usuario con ID {request.AssignedToId} no existe.");

    var entity = AssignmentEntity.Create(
                  AssignmentId.Create(), 
                  userId,
                  new AssignmentTitle(request.Title),
                  new AssignmentDescription(request.Description),
                  createAt,
                  new AssignmentDueAt(request.DueAt, createAt));

    await _repository.RegisterAsync(entity);
    await _unitOfWork.SaveChangesAsync();

    return entity.Id.ToString();
  }
}