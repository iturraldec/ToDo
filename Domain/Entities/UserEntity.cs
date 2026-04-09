using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class UserEntity
{
  public UserId Id { get;}
  
  public UserName Name { get; private set; }
  
  public UserEmail Email { get; private set; }
  
  public UserRole Role { get; private set; }
  
  public UserEntity(UserId id, UserName name, UserEmail email, UserRole role)
  {    
    Id = id;
    Name = name;
    Email = email;
    Role = role;
  }

  public void Update(UserName name, UserEmail email, UserRole role)
  {
    Name = name;
    Email = email;
    Role = role;
  }
}