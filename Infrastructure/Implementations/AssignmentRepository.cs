using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Implementations;

public class AssignmentRepository : IRepository<AssignmentEntity, AssignmentId>
{
  private readonly ToDoContext _context;
  private readonly IUnitOfWork _unitOfWork;

  public AssignmentRepository(ToDoContext context, IUnitOfWork unitOfWork)
  {
    _context = context;
    _unitOfWork = unitOfWork;
  }
  public async Task RegisterAsync(AssignmentEntity assignment)
  {
    var model = new Assignment
    {
        Id = assignment.Id.Value,
        UserId = assignment.Id.Value,
        Title = assignment.Title.Value,
        Description = assignment.Description.Value,
        Status = (short)assignment.Status.Value,
        CreatedAt = assignment.CreatedAt,
        DueAt = assignment.DueAt,
    };

    await _context.Assignments.AddAsync(model);
  }

  public async Task<IReadOnlyList<AssignmentEntity>> GetAllAsync()=> throw new NotImplementedException();

  public async Task<AssignmentEntity?> GetByIdAsync(AssignmentId id) => throw new NotImplementedException();
  
  public async Task UpdateAsync(AssignmentEntity assignment)=> throw new NotImplementedException();

  public async Task DeleteAsync(AssignmentEntity entity) => await _context.Assignments.Where(a => a.Id == entity.Id.Value).ExecuteDeleteAsync();
}