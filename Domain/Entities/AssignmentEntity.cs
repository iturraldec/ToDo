using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;
public class AssignmentEntity
{
  public AssignmentId Id { get; private set; }
  public UserId AssignmentToId { get; private set; }
  public AssignmentTitle Title { get; private set; }
  public AssignmentDescription Description { get; private set; }
  public AssignmentStatus Status { get; private set; }
  public AssignmentCreadtedAt CreatedAt { get; private set; }
  public AssignmentDueAt DueAt { get; private set; }

  // contructor privado para forzar el uso de la fábrica
  private AssignmentEntity(AssignmentId id, UserId userId, AssignmentTitle title, AssignmentDescription description, AssignmentStatus status, AssignmentCreadtedAt createdAt, AssignmentDueAt dueAt)
  {
    Id = id;
    AssignmentToId = userId;
    Title = title;
    Description = description;
    Status = status;
    CreatedAt = createdAt;
    DueAt = dueAt;
  }
  // validar que la fecha de vencimiento no sea anterior a la fecha de creación
  private static void _ValidateDueDate(AssignmentCreadtedAt createdAt, AssignmentDueAt dueAt)
  {
    if (dueAt.Value < DateOnly.FromDateTime(createdAt.Value)) throw new ArgumentException($"La fecha de vencimiento ({dueAt.Value}) no puede ser anterior a la creación ({DateOnly.FromDateTime(createdAt.Value)}).");
  }
  // fábrica para crear una nueva asignación
  public static AssignmentEntity Create(
                  AssignmentId id, 
                  UserId userId, 
                  AssignmentTitle title, 
                  AssignmentDescription description, 
                  AssignmentCreadtedAt createdAt, 
                  AssignmentDueAt dueAt)
    {
      _ValidateDueDate(createdAt, dueAt);

      return new(id, 
                userId, 
                title, 
                description, 
                new AssignmentStatus(AssignmentStatusEnum.Pending), 
                createdAt, 
                dueAt
              );
  }
  // fábrica para crear una asignación desde la persistencia (sin validaciones, asumiendo que los datos ya son válidos)
  public static AssignmentEntity FromPersistence(AssignmentId id, UserId userId, AssignmentTitle title, AssignmentDescription description, 
                  AssignmentStatus status, AssignmentCreadtedAt createdAt, AssignmentDueAt dueAt) 
                  => new(id, userId, title, description, status, createdAt, dueAt);
  // método para actualizar el estado de la asignación
  public void ChangeStatus(AssignmentStatus newStatus, UserRolesEnum userRole)
  {
      bool isAdmin = userRole == UserRolesEnum.Admin;

      // "Status" es la propiedad actual de la entidad Tarea
      bool isValid = (Status.Value, newStatus.Value) switch
      {
          // Reglas generales (Cualquier rol)
          (AssignmentStatusEnum.Pending, AssignmentStatusEnum.InProgress) => true,
          (AssignmentStatusEnum.InProgress, AssignmentStatusEnum.Completed) => true,

          // Reglas exclusivas de Administrador
          (AssignmentStatusEnum.Pending, AssignmentStatusEnum.Archived) when isAdmin => true,
          (AssignmentStatusEnum.InProgress, AssignmentStatusEnum.Archived) when isAdmin => true,
          (AssignmentStatusEnum.Completed, AssignmentStatusEnum.InProgress) when isAdmin => true,

          // No hay cambio (Quedarse en el mismo estado)
          _ when Status.Value == newStatus.Value => true,

          // Cualquier otro caso es inválido
          _ => false
      };

      if (!isValid)
      {
          throw new InvalidActionException(
              $"Transición no permitida: {Status} -> {newStatus}. " +
              $"El rol '{userRole}' no tiene permisos para esta acción específica."
          );
      }

      Status = newStatus;
  }
  // cambiar fecha de entrega, validando que no sea anterior a la fecha de creación
  public void ChangeDueDate(AssignmentDueAt newDueAt)
  {
    _ValidateDueDate(CreatedAt, newDueAt);
    DueAt = newDueAt;
  }
}