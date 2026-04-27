namespace Domain.ValueObjects;
public record AssignmentDueAt
{
  public DateOnly Value { get; init; } 
  public AssignmentDueAt(DateOnly value)
  {
    if (value == DateOnly.MinValue) throw new ArgumentException("La fecha de entregadebe ser una fecha válida y no vacía.", nameof(value));

    Value = value;
  }
}