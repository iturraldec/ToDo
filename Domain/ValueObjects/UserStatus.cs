using Domain.Enums;

namespace Domain.ValueObjects;

public record UserStatus
{
  public UserStatusEnum Value { get; init; }
  
  public UserStatus(UserStatusEnum value)
  {
    if (!Enum.IsDefined(typeof(UserStatusEnum), value)) throw new ArgumentException("El estado del usuario no es válido.");

    Value = value;
  }

  public override string ToString() => Value switch 
  {
      UserStatusEnum.Active   => "Activo",
      UserStatusEnum.Inactive => "Inactivo"
  };
}