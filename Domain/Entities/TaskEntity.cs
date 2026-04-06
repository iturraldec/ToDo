using Domain.Enums;

namespace Domain.Entities;

public class TaskEntity
{
  public Guid Id { get; set; }
  
  public string Title { get; set; }
  
  public string Description { get; set; }

  public TaskStatus Status { get; set; }
  
  public DateTime CreatedAt { get; set; }

  public Guid AssignedById { get; set; }

  public Guid AssignedToId { get; set; }
}