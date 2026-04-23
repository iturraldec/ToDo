namespace Domain.ValueObjects;

public record AssignmentId
{
  public Guid Value { get; init; } 
  public AssignmentId(Guid value)
  {
    if (value == Guid.Empty) throw new ArgumentException("El AssignmentId debe ser un GUID válido y no vacío.", nameof(value));

    Value = value;
  }
  public static AssignmentId Create() => new(Guid.NewGuid());
  public override string ToString() => Value.ToString();
}