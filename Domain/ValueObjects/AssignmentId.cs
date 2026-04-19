namespace Domain.ValueObjects;

public record AssignmentId
{
  public Guid Value { get; } 

  private AssignmentId(Guid value) => Value = value;

  public static AssignmentId FromGuid(Guid value)
  {
    if (value == Guid.Empty) throw new ArgumentException("El Id de la tarea no puede ser un GUID vacío.", nameof(value));

    return new AssignmentId(value);
  }

  public static AssignmentId Create() => new(Guid.NewGuid());

  public static implicit operator Guid(AssignmentId assignmentId) => assignmentId.Value;
  
  public override string ToString() => Value.ToString();
}