using Domain.Enums;

namespace Domain.ValueObjects;

public record UserRole
{
  public Roles Value { get; }
  
  public UserRole(Roles value)
  {    
    if (!Enum.IsDefined(typeof(Roles), value)) throw new ArgumentException("Rol no válido.");

    Value = value;
  }

  public bool IsAdmin() => Value == Roles.Admin;
  
  public bool IsOperator() => Value == Roles.Operator;
}