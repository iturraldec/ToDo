using Domain.Enums;

namespace Domain.ValueObjects;

public record UserRole
{
  public UserRolesEnum Value { get; init; }

  public UserRole(UserRolesEnum value)
  {
    if (!Enum.IsDefined(typeof(UserRolesEnum), value)) throw new ArgumentException("Rol de usuario no válido.");

    Value = value;
  }

  public bool IsAdmin() => Value == UserRolesEnum.Admin;
  
  public bool IsOperator() => Value == UserRolesEnum.Operator;

  public override string ToString() => Value switch 
  {
      UserRolesEnum.Admin    => "Administrador",
      UserRolesEnum.Operator => "Operador"
  };
}