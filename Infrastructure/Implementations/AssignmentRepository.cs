using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Implementations;

public class AssignmentRepository : IRepository<AssignmentEntity, AssignmentId>, IAssignmentReads
{
  private readonly ToDoContext _context;
  private readonly IUnitOfWork _unitOfWork;
  public AssignmentRepository(ToDoContext context, IUnitOfWork unitOfWork)
  {
    _context = context;
    _unitOfWork = unitOfWork;
  }
  public async Task<AssignmentEntity?> GetByIdAsync(AssignmentId id) => throw new NotImplementedException();
  public async Task<AssignmentResponse?> GetDetailsByIdAsync(Guid id)
  {
    var model = await _context.Assignments.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    if(model == null) return null;

    return new AssignmentResponse(
                "nombre del responsable", // AssignmentName
                model.Title,               // Title
                model.Description,         // Description
                new AssignmentStatus((AssignmentStatusEnum)model.Status).ToString(), // Status
                model.CreatedAt,           // CreatedAt
                model.DueAt                // DueAt
            );
  }
  public Task<IReadOnlyList<AssignmentResponse>> GetAllAssignmentsAsync() => throw new NotImplementedException();
  public async Task RegisterAsync(AssignmentEntity assignment)
  {
    var model = new Assignment
    {
        Id = assignment.Id.Value,
        UserId = assignment.Id.Value,
        Title = assignment.Title.Value,
        Description = assignment.Description.Value,
        Status = (short)assignment.Status.Value,
        CreatedAt = assignment.CreatedAt.Value,
        DueAt = assignment.DueAt.Value,
    };

    await _context.Assignments.AddAsync(model);
  }  
  public async Task UpdateAsync(AssignmentEntity assignment)=> throw new NotImplementedException();
  public async Task DeleteAsync(AssignmentEntity entity) => await _context.Assignments.Where(a => a.Id == entity.Id.Value).ExecuteDeleteAsync();
}