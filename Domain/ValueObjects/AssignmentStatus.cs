using Domain.Enums;

namespace Domain.ValueObjects;

public record AssignmentStatus
{
  public AssignmentStatusEnum Value { get; init; }
  public AssignmentStatus(AssignmentStatusEnum value)
  {
    if (!Enum.IsDefined(typeof(AssignmentStatusEnum), value)) throw new ArgumentException("El estado de la tarea no es válido.");

    Value = value;
  }
  public override string ToString() => Value switch 
  {
      AssignmentStatusEnum.Pending => "Pendiente",
      AssignmentStatusEnum.InProgress => "En curso",
      AssignmentStatusEnum.Completed => "Completada",
      AssignmentStatusEnum.Archived => "Cancelada"
  };
}