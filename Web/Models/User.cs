using System;
using System.Collections.Generic;

namespace Web.Models;

public partial class User
{
    /// <summary>
    /// Nombre del usuario.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Email del usuario.
    /// </summary>
    public string Email { get; set; } = null!;

    /// <summary>
    /// Rol del usuario.
    /// </summary>
    public short Role { get; set; }

    /// <summary>
    /// Fecha y hora de creacion del usuario.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Id del usuario.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Fecha y hora de la ultima modificacion del usuario.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    public short Status { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}
