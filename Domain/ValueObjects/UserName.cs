namespace Domain.ValueObjects;

public record UserName
{  
  public string Value { get; }  

  public UserName(string value)
  {
    if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El nombre de usuario es requerido.");
 
    Value = value;
  }
}