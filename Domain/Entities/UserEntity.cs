using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserEntity
{
  public UserId Id { get; private set; }
  public UserRole Role { get; private set; }
  public UserName Name { get; private set; }
  public UserEmail Email { get; private set; }
  public UserStatus Status { get; private set; }
  private UserEntity(UserId id, UserRole role, UserName name, UserEmail email, UserStatus status)
  {    
    Id = id;
    Role = role;
    Name = name;
    Email = email;
    Status = status;
  }

  // por los momentos sin logica de negocio a validar, pero se puede agregar en el futuro
  public static UserEntity Create(UserId id, UserRole role, UserName name, UserEmail email) => new(id, role, name, email, new UserStatus(UserStatusEnum.Active));
  public static UserEntity FromPersistence(UserId id, UserRole role, UserName name, UserEmail email, UserStatus status) => new(id, role, name, email, status); 
  public void ChangeName(UserName newName)
  {
    if (Name.Equals(newName)) return;
    
    Name = newName;
    // Aquí podrías, en el futuro, registrar un evento: 
    // AddDomainEvent(new UserNameChanged(Id, newName));
  }
}