using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Implementations;

public class AssignmentRepository : IAssignmentRepository, IAssignmentReads
{
  private readonly ToDoContext _context;
  private readonly IUnitOfWork _unitOfWork;
  public AssignmentRepository(ToDoContext context, IUnitOfWork unitOfWork)
  {
    _context = context;
    _unitOfWork = unitOfWork;
  }
  public async Task<AssignmentEntity?> GetByIdAsync(AssignmentId id)
  {
    var model = await _context.Assignments.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id.Value);

    if (model == null) return null;

    return AssignmentEntity.FromPersistence(
                    new AssignmentId(model.Id),
                    new UserId(model.UserId),
                    new AssignmentTitle(model.Title),
                    new AssignmentDescription(model.Description),
                    new AssignmentStatus((AssignmentStatusEnum)model.Status),
                    new AssignmentCreadtedAt(model.CreatedAt),
                    new AssignmentDueAt(model.DueAt)
                );
  }
  public async Task<AssignmentResponse?> GetDetailsByIdAsync(Guid id)
  {
    var model = await _context.Assignments
                              .Include(a => a.User)
                              .AsNoTracking()
                              .FirstOrDefaultAsync(u => u.Id == id);

    if(model == null) return null;

    return new AssignmentResponse(
                model.Id.ToString(),
                model.UserId.ToString(),
                model.User.Name,
                model.Title,     
                model.Description,
                new AssignmentStatus((AssignmentStatusEnum)model.Status).ToString(),
                model.CreatedAt,
                model.DueAt
            );
  }
  public async Task<IReadOnlyList<AssignmentResponse>> GetAllAssignmentsAsync()
  {
    var modelos = await _context.Assignments
                         .OrderByDescending(a => a.CreatedAt)
                         .AsNoTracking()
                         .Select(model => new AssignmentResponse(
                            model.Id.ToString(),
                            model.UserId.ToString(),
                            model.User.Name,
                            model.Title,           
                            model.Description,
                            new AssignmentStatus((AssignmentStatusEnum)model.Status).ToString(),
                            model.CreatedAt,
                            model.DueAt
                        )).ToListAsync();

    return modelos.AsReadOnly();
  }
  public async Task<IReadOnlyList<AssignmentResponse>> GetByUserIdAssignmentsAsync(Guid userId)
  {
    var modelos = await _context.Assignments
                         .OrderByDescending(a => a.CreatedAt)
                         .AsNoTracking()
                         .Where(a => a.UserId == userId)
                         .Select(model => new AssignmentResponse(
                            model.Id.ToString(),
                            model.UserId.ToString(),
                            model.User.Name,
                            model.Title,           
                            model.Description,
                            new AssignmentStatus((AssignmentStatusEnum)model.Status).ToString(),
                            model.CreatedAt,
                            model.DueAt           
                        )).ToListAsync();

    return modelos.AsReadOnly();
  }
  public async Task RegisterAsync(AssignmentEntity assignment)
  {
    var model = new Assignment
    {
        Id = assignment.Id.Value,
        UserId = assignment.AssignmentToId.Value,
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
  public async Task ChangeStatusAsync(AssignmentEntity assignment, AssignmentStatusEnum newStatus, UserRolesEnum userRole) => throw new NotImplementedException();
  public async Task ChangeDueDateAsync(AssignmentEntity assignment, AssignmentDueAt newDueAt)
  {
    var existingModel = await _context.Assignments.FindAsync(assignment.Id.Value);
    
    if (existingModel != null)
    {
        existingModel.DueAt = newDueAt.Value;
        existingModel.UpdatedAt = DateTime.Now;
    }
  }
}