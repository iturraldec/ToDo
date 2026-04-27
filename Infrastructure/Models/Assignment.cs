using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Assignment
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public short Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid UserId { get; set; }

    public DateOnly DueAt { get; set; }

    public virtual User User { get; set; } = null!;
}
