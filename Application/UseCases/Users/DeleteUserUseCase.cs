using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.UseCases.Users;
public class DeleteUserUseCase
{
  private readonly IUserRepository _repository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteUserUseCase(IUserRepository repository, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _unitOfWork = unitOfWork;
  }

  public async Task Execute(Guid id)
  {
    var entity = await _repository.GetByIdAsync(new UserId(id));

    if (entity == null) throw new NotFoundException($"El usuario con ID {id} no existe.");

    await _repository.DeleteAsync(entity);
    
    await _unitOfWork.SaveChangesAsync();
  }
}