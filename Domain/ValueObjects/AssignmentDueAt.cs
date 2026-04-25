namespace Domain.ValueObjects;
public record AssignmentDueAt
{
  public DateTime Value { get; init; } 
  public AssignmentDueAt(DateTime value, AssignmentCreadtedAt createdAt)
  {
    if (value == DateTime.MinValue) throw new ArgumentException("El AssignmentDueAt debe ser una fecha válida y no vacía.", nameof(value));
    if (value < createdAt.Value) throw new ArgumentException("La fecha de vencimiento debe ser posterior a la fecha de creación.", nameof(value));

    Value = value;
  }
}