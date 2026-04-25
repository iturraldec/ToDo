using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;

namespace Application.UseCases.Assignments;
public class GetDetailsByIdAssignmentUseCase
{
  private readonly IAssignmentReads _assignmentReads;

  public GetDetailsByIdAssignmentUseCase(IAssignmentReads assignmentReads) => _assignmentReads = assignmentReads;
  public async Task<AssignmentResponse?> Execute(Guid id)
  {
    var response = await _assignmentReads.GetDetailsByIdAsync(id);

    if(response is null) throw new NotFoundException("Assignment not found");
 
    return response;
  }
}