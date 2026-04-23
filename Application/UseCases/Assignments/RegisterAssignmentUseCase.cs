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
    if (! await _userReadService.IsIdExistsAsync(new UserId(request.AssignedToId))) throw new NotFoundException($"El usuario con ID {request.AssignedToId} no existe.");

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