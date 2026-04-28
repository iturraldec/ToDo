using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.UseCases.Assignments;
public class ChangeAssignmentStatusUseCase
{  
  private readonly IAssignmentRepository _assignmentRepository;
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  public ChangeAssignmentStatusUseCase(IAssignmentRepository assignmentRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
  {
    _assignmentRepository = assignmentRepository;
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }
  public async Task Execute(ChangeAssignmentStatusRequest request)
  {
    var assignment = await _assignmentRepository.GetByIdAsync(new AssignmentId(request.AssignmentId));
    
    if (assignment == null) throw new KeyNotFoundException($"No se encontró la asignación con ID {request.AssignmentId}.");

    var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));
    
    if (user == null) throw new KeyNotFoundException($"No se encontró el usuario con ID {request.UserId}.");

    // Cambiar el estado de la asignación
    assignment.ChangeStatus(new AssignmentStatus((AssignmentStatusEnum)request.newStatus), user.Role.Value);

    // Guardar los cambios en la base de datos
    await _assignmentRepository.ChangeStatusAsync(assignment, new AssignmentStatus((AssignmentStatusEnum)request.newStatus));
   
    await _unitOfWork.SaveChangesAsync();
  }
}