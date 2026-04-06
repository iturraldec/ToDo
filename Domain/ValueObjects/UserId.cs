namespace Domain.ValueObjects;

public record UserId
{
  public Guid Value { get; } 

  private UserId(Guid value) => Value = value;

  public static UserId FromGuid(Guid value)
  {
    if (value == Guid.Empty)
        throw new ArgumentException("El UserId no puede ser un GUID vacío.", nameof(value));

    return new UserId(value);
  }

  public static UserId Create() => new(Guid.NewGuid());

  // Permite usar el objeto como un Guid automáticamente cuando sea necesario
  public static implicit operator Guid(UserId userId) => userId.Value;
  
  public override string ToString() => Value.ToString();
}