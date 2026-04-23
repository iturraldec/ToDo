namespace Domain.ValueObjects;

public record UserId
{
  public Guid Value { get; init; } 
  public UserId(Guid value)
  {
    if (value == Guid.Empty) throw new ArgumentException("El UserId debe ser un GUID válido y no vacío.", nameof(value));

    Value = value;
  }
  public static UserId Create() => new(Guid.NewGuid());
  public override string ToString() => Value.ToString();
}