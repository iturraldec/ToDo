namespace Domain.ValueObjects;

public record AssignmentTitle
{
  private short MinLength => 5;
  private short MaxLength => 200;
  public string Value { get; init; }
  public AssignmentTitle(string value)
  {
    if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength || value.Length > MaxLength) 
      throw new ArgumentException($"El título debe tener entre {MinLength} y {MaxLength} caracteres.");

    Value = value;
  }
}