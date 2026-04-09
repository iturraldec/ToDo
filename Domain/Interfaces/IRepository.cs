using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IRepository<T, TId> where T : class
{
  Task<T?> GetByIdAsync(TId id);

  Task<IReadOnlyList<T>> GetAllAsync();

  Task AddAsync(T entity);

  Task UpdateAsync(T entity);

  Task DeleteAsync(TId id);
}