namespace Domain.ValueObjects;

public record AssignmentCreadtedAt
{
  public DateTime Value { get; init; } 
  public AssignmentCreadtedAt(DateTime value)
  {
    if (value == DateTime.MinValue) throw new ArgumentException("El AssignmentCreatedAt debe ser una fecha válida y no vacía.", nameof(value));

    Value = value;
  }
  public static AssignmentCreadtedAt Create() => new(DateTime.Now);
}