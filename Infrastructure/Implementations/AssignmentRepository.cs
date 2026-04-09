using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Implementations;

public class AssignmentRepository : IRepository<AssignmentEntity, Guid>
{
  private readonly ToDoContext context;

  public AssignmentRepository(ToDoContext context)
  {
      this.context = context;
  }

  public async Task AddAsync(AssignmentEntity assignment)
  {
    var model = new Assignment
    {
        Id = assignment.Id,
        Title = assignment.Title,
        Description = assignment.Description,
        Status = (short)assignment.Status,
        CreatedAt = assignment.CreatedAt,
        AssignedById = assignment.AssignedById,
        AssignedToId = assignment.AssignedToId
    };

    await context.Assignments.AddAsync(model);
    await context.SaveChangesAsync();
  }

  public async Task<IReadOnlyList<AssignmentEntity>> GetAllAsync()
  {
    var models = await context.Assignments.AsNoTracking().ToListAsync();

    return models.Select(model => new AssignmentEntity
    {
      Id = model.Id,
      Title = model.Title,
      Description = model.Description,
      Status = (AssignmentStatus)model.Status,
      CreatedAt = model.CreatedAt,
      AssignedById = model.AssignedById,
      AssignedToId = model.AssignedToId
    }).ToList().AsReadOnly();
  }

  public async Task<AssignmentEntity?> GetByIdAsync(Guid id)
  {
    var model = await context.Assignments.FindAsync(id);

    if (model == null) return null;

    return new AssignmentEntity
    {
      Id = model.Id,
      Title = model.Title,
      Description = model.Description,
      Status = (AssignmentStatus)model.Status,
      CreatedAt = model.CreatedAt,
      AssignedById = model.AssignedById,
      AssignedToId = model.AssignedToId
    };
  }

  public async Task UpdateAsync(AssignmentEntity assignment)
  {
    var existingAssignment = await context.Assignments.FindAsync(assignment.Id);

    if (existingAssignment != null)
    {
        existingAssignment.Title = assignment.Title;
        existingAssignment.Description = assignment.Description;
        existingAssignment.Status = (short)assignment.Status;
        existingAssignment.AssignedById = assignment.AssignedById;
        existingAssignment.AssignedToId = assignment.AssignedToId;
        existingAssignment.UpdatedAt = DateTime.Now;

        await context.SaveChangesAsync();
    }
  }

  public async Task DeleteAsync(Guid id)
  {
      var assignment = await context.Assignments.FindAsync(id);

      if (assignment != null)
      {
        context.Assignments.Remove(assignment);
        await context.SaveChangesAsync();
      }
  }
}