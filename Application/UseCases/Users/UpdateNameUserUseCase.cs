using Application.DTOs;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;

public class UpdateNameUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  private readonly IUserRepository _userRepository;

  public UpdateNameUserUseCase(IRepository<UserEntity, UserId> repository, IUserRepository userRepository)
  {
    _repository = repository;
    _userRepository = userRepository;
  }

  public async Task Execute(UserUpdateNameRequest request)
  {
    var id = new UserId(request.Id);
    
    var entity = await _repository.GetByIdAsync(id);
    
    var name = new UserName(request.Name);

    entity.UpdateName(name);

    await _userRepository.UpdateNameAsync(name);
  }
}