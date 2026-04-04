
namespace Infrastructure.Models;

public partial class User
{
    /// <summary>
    /// Id del usuario.
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Fecha y hora de ultima modificacion del usuario.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}
