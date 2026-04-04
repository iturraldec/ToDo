namespace Domain.ValueObjects;
public record UserEmail
{  
  public string Value { get; private set; }  
  public UserEmail(string value)
  {
    if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El email es requerido.");
    Value = value;
  }
}