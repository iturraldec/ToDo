using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Interfaces;

public interface IUserRepository : IRepository<UserEntity, UserId>
{
  Task ChangeNameAsync(UserEntity user);
}