namespace Domain.ValueObjects;
public record AssignmentDueAt
{
  public DateOnly Value { get; init; } 
  public AssignmentDueAt(DateOnly value)
  {
    if (value == null) throw new ArgumentException("La fecha de entrega debe ser una fecha válida y no vacía.", nameof(value));

    Value = value;
  }
}