using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Models;

namespace Infrastructure.Implementations;
public class UserRepository : IUserRepository, IUserReads
{
  private readonly ToDoContext _context;
  private readonly IUnitOfWork _unitOfWork;
  public UserRepository(ToDoContext context, IUnitOfWork unitOfWork)
  {
    _context = context;
    _unitOfWork = unitOfWork;
  }
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
  public async Task<UserResponse?> GetDetailsByIdAsync(Guid id)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    if (model == null) return null;

    return new UserResponse(
                model.Id.ToString(),
                new UserRole((UserRolesEnum)model.Role).ToString(),
                model.Name,
                model.Email,
                new UserStatus((UserStatusEnum)model.Status).ToString(),
                model.CreatedAt,
                model.UpdatedAt
          );
  }
  public async Task<bool> IsIdExistsAsync(UserId id)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id.Value);

    return model != null;
  }
  public async Task<bool> IsEmailExistsAsync(string email)
  {
    var model = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

    return model != null;
  }
  public async Task<IReadOnlyList<UserResponse>> GetAllUsersAsync()
  {

    var userModels = await _context.Users.OrderBy(u => u.Name).AsNoTracking().ToListAsync();

    return userModels.Select(model => new UserResponse(
                                            model.Id.ToString(),
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

  public async Task RegisterAsync(UserEntity entity)
  {
    var userModel = new User {
                          Id = entity.Id.Value,
                          Name = entity.Name.Value,
                          Email = entity.Email.Value,
                          Role = (short) entity.Role.Value,
                          Status = (short) entity.Status.Value,
                          CreatedAt = DateTime.Now
                        };
    
    await _context.Users.AddAsync(userModel);
  }
  public async Task ChangeNameAsync(UserEntity entity)
  {
    var existingModel = await _context.Users.FindAsync(entity.Id.Value);
    
    if (existingModel != null)
    {
        existingModel.Name = entity.Name.Value;
        existingModel.UpdatedAt = DateTime.Now;
    }
  }
  public async Task DeleteAsync(UserEntity entity) => await _context.Users.Where(u => u.Id == entity.Id.Value).ExecuteDeleteAsync();
}