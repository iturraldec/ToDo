#pragma warning disable IDE0005
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;
public class UserEntity
{
  public UserId Id { get; private set; }
  public UserName Name { get; private set; }
  public UserEmail Email { get; private set; }
  public Roles Role { get; private set; }
  public UserEntity(UserId id, UserName name, UserEmail email, Roles role)
  {
    if (!Enum.IsDefined(typeof(Roles), role)) throw new ArgumentException("Rol no válido.");
    Id = id;
    Name = name;
    Email = email;
    Role = role;
  }
  public bool IsAdmin() => Role == Roles.Admin;
  public bool IsOperator() => Role == Roles.Operator;
}