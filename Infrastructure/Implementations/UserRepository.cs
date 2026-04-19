using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Models;

namespace Infrastructure.Implementations;

public class UserRepository : IUserRepository, IUserReads
{
  private readonly ToDoContext _context;

  public UserRepository(ToDoContext context) =>_context = context;

  public async Task<UserEntity?> GetByIdAsync(UserId id)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id.Value);

    if (model == null) return null;

    return UserEntity.FromPersistence(
                    new UserId(model.Id),
                    new UserRole((UserRolesEnum)model.Role),
                    new UserName(model.Name),
                    new UserEmail(model.Email),
                    new UserStatus((UserStatusEnum)model.Status)
                );
  }
  public async Task<bool> IsEmailExistsAsync(string email)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    return model != null;
  }
  public async Task<IReadOnlyList<UserResponse>> GetAllAsync()
  {

    var userModels = await _context.Users.OrderBy(u => u.Name).AsNoTracking().ToListAsync();

    return userModels.Select(model => new UserResponse(
                                            new UserRole((UserRolesEnum)model.Role).ToString(),
                                            model.Name,
                                            model.Email,
                                            new UserStatus((UserStatusEnum)model.Status).ToString(),
                                            model.CreatedAt,
                                            model.UpdatedAt
                                  ))
                                  .ToList()
                                  .AsReadOnly();
  }

  public async Task AddAsync(UserEntity entity)
  {
    var userModel = new User
    {
      Id = entity.Id.Value,
      Name = entity.Name.Value,
      Email = entity.Email.Value,
      Role = (short) entity.Role.Value,
      Status = (short) entity.Status.Value,
      CreatedAt = DateTime.Now
    };
    
    await _context.Users.AddAsync(userModel);
    await _context.SaveChangesAsync();
  }
  public async Task UpdateNameAsync(UserName name)
  {
    var existingModel = await _context.Users.FindAsync(name.Value);
    
    if (existingModel != null)
    {
        existingModel.Name = name.Value;
        existingModel.UpdatedAt = DateTime.Now;

        await SaveChangesAsync();
    }
  }
  public async Task DeleteAsync(UserId id) => await _context.Users.Where(u => u.Id == id.Value).ExecuteDeleteAsync();
  public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}