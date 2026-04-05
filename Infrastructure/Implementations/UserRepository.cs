using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Models;

namespace Infrastructure.Implementations;
public class UserRepository : IRepository<UserEntity, UserId>
{
  private readonly ToDoContext _context;
  public UserRepository(ToDoContext context)
  {
      _context = context;
  }
  public async Task<UserEntity?> GetByIdAsync(UserId id)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id.Value);

    if (model == null) return null;

    return new UserEntity(
                    UserId.FromGuid(model.Id),
                    new UserName(model.Name),
                    new UserEmail(model.Email),
                    (Roles) model.Role
                );
  }

  public async Task<UserEntity?> GetByEmailAsync(UserEmail email)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email.Value);

    if (model == null) return null;

    return new UserEntity(
                    UserId.FromGuid(model.Id),
                    new UserName(model.Name),
                    new UserEmail(model.Email),
                    (Roles) model.Role
                );
  }
  public async Task<IReadOnlyList<UserEntity>> GetAllAsync()
  {

    var userModels = await _context.Users.AsNoTracking().ToListAsync();

    return userModels.Select(model => new UserEntity(
        UserId.FromGuid(model.Id),
        new UserName(model.Name),
        new UserEmail(model.Email),
        (Roles) model.Role
    )).ToList().AsReadOnly();
  }
  public async Task AddAsync(UserEntity entity)
  {
    var user = new User
    {
      Id = UserId.CreateUnique().Value,
      Name = entity.Name.Value,
      Email = entity.Email.Value,
      Role = (short) entity.Role
    };
    
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
  }
  public async Task UpdateAsync(UserEntity entity)
  {
    var existingModel = await _context.Users.FindAsync(entity.Id.Value);
    
    if (existingModel != null)
    {
        existingModel.Name = entity.Name.Value;
        existingModel.Email = entity.Email.Value;
        existingModel.Role = (short)entity.Role;

        await _context.SaveChangesAsync();
    }
  }
  public async Task DeleteAsync(UserId id)
  {
    var entity = await GetByIdAsync(id);

    var user = new User
    {
      Id = entity.Id.Value,
      Name = entity.Name.Value,
      Email = entity.Email.Value,
      Role = (short)entity.Role
    };

    _context.Users.Remove(user);
    await _context.SaveChangesAsync();
  }
}