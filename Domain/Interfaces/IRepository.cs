using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IRepository<T, TId> where T : class
{
  Task RegisterAsync(T entity);
  Task<T?> GetByIdAsync(TId id);
  Task DeleteAsync(T entity);
}