namespace Domain.ValueObjects;

public record AssignmentId
{
  public Guid Value { get; init; } 
  public AssignmentId(Guid value)
  {
    if (value == Guid.Empty) throw new ArgumentException("El AssignmentId debe ser un GUID válido y no vacío.", nameof(value));

    Value = value;
  }
  public static AssignmentId Unique() => new(Guid.NewGuid());
  public override string ToString() => Value.ToString();
  public bool ChangeStatusTo(AssignmentStatus newStatus)
  {
    // Aquí puedes implementar la lógica para cambiar el estado del AssignmentId
    // Por ejemplo, podrías tener un campo adicional para el estado y actualizarlo aquí
    // return true si el cambio de estado fue exitoso, o false si no lo fue
    return true; // Placeholder, implementa la lógica real según tus necesidades
  }
}