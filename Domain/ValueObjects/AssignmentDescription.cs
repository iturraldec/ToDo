namespace Domain.ValueObjects;

public record AssignmentDescription
{
  public string Value { get; init; }
  public AssignmentDescription(string value)
  {
    if (string.IsNullOrWhiteSpace(value)) 
      throw new ArgumentException($"La descripción no puede estar vacía.");

    Value = value;
  }
}