using Application.DTOs;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;
public class UpdateUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  public UpdateUserUseCase(IRepository<UserEntity, UserId> repository) => _repository = repository;
  public async Task Execute(UserRequest request)
  {
    var userId = UserId.FromGuid(request.Id.Value);
    var entity = await _repository.GetByIdAsync(userId);
    
    if (entity == null) throw new Exception("Usuario no encontrado");

    // 2. El dominio se actualiza a sí mismo (aquí irían validaciones de negocio)
    entity.UpdateInfo(
        new UserName(request.Name),
        new UserEmail(request.Email),
        (Roles) request.Role
    );

    // 3. Guardar los cambios
    await _repository.UpdateAsync(entity);
  }
}