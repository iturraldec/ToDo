#pragma warning disable IDE0005
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
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
  /* public async Task<User?> GetByIdAsync(UserId id)
  {
      // EF Core usará el Value Converter que definimos en el DbContext
      return await _context.Users.FindAsync(id);
  } */

  /* public async Task<IEnumerable<User>> GetAllAsync()
  {
      return await _context.Users.ToListAsync();
  } */

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

  /* public async Task UpdateAsync(User user)
  {
      _context.Users.Update(user);
      await _context.SaveChangesAsync();
  }

  public async Task DeleteAsync(UserId id)
  {
      var user = await GetByIdAsync(id);
      if (user != null)
      {
          _context.Users.Remove(user);
          await _context.SaveChangesAsync();
      }
  } */
}