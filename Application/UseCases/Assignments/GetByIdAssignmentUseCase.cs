using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.UseCases.Assignments;

public class GetByIdAssignmentUseCase
{
  private readonly IRepository<AssignmentEntity, Guid> _repository;

  public GetByIdAssignmentUseCase(IRepository<AssignmentEntity, Guid> repository)
  {
      _repository = repository;
  }

  public async Task<AssignmentResponse?> Execute(Guid id)
  {
    var model = await _repository.GetByIdAsync(id);

    if (model is null) return null;

    return new AssignmentResponse(
                model.Title,
                model.Description,
                model.Status.ToString(),
                // Extraemos los nombres de los modelos cargados por el Include
                model.AssignedUser?.Name ?? "N/A", 
                model.AssignedToUser?.Name ?? "N/A"
            );
    }
}