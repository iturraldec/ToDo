using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Infrastructure.Implementations;
public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoContext _context;

    public UnitOfWork(ToDoContext context) => _context = context;

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
      _context.Dispose();
      GC.SuppressFinalize(this);
    }
}