using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class AssignmentEntity
{
  public AssignmentId Id { get; }
  public UserId UserId { get; private set; }    // El responsable
  public UserId CreatorId { get; }              // Quién la asignó
  public string Title { get; private set; }
  public string? Description { get; private set; }
  public AssignmentStatus Status { get; private set; }
  public DateTime CreatedAt { get; }

  public AssignmentEntity(
    AssignmentId id, 
    UserId userId, 
    UserId creatorId, 
    string title, 
    string? description)
  {
    // 1. Validar si el creador es Operador
    if (creatorRole.IsOperator() && creatorId != userId)
    {
        throw new DomainException("Un operador solo puede asignarse tareas a sí mismo.");
    }

    // 2. Validar si el creador es Admin
    if (creatorRole.IsAdmin() && creatorId != userId && userRole.IsAdmin())
    {
        throw new DomainException("Un administrador no puede asignar tareas a otro administrador.");
    }
    
    Id = id;
    UserId = userId;
    CreatorId = creatorId;
    Title = title;
    Description = description;
    Status = AssignmentStatus.Pending; // Nace siempre en Pending
    CreatedAt = DateTime.UtcNow;
  }
}