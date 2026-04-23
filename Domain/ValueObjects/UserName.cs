namespace Domain.ValueObjects;

public record UserName
{
  private short MinLength => 3;
  private short MaxLength => 50;
  public string Value { get; init; }
  public UserName(string value)
  {
    if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength || value.Length > MaxLength) 
      throw new ArgumentException($"El nombre debe tener entre {MinLength} y {MaxLength} caracteres.");

    Value = value;
  }
}