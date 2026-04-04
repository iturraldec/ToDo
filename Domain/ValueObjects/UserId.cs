namespace Domain.ValueObjects;
public record UserId
{
  public Guid Value { get; init; }
  private UserId(Guid value) => Value = value;
  public static UserId FromGuid(Guid value)
  {
    if (value == Guid.Empty)
      throw new ArgumentException("El GUID no puede ser vacío.", nameof(value));

    return new UserId(value);
  }
  public static UserId CreateUnique() => new(Guid.NewGuid());
}