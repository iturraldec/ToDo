using Application.DTOs;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;
using Domain.Exceptions;

namespace Application.UseCases.Users;
public class ChangeNameUserUseCase
{
  private readonly IUserRepository _userRepository;
  private readonly IUnitOfWork _unitOfWork;
  public ChangeNameUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
  {
      _userRepository = userRepository;
      _unitOfWork = unitOfWork;
  }

  public async Task Execute(ChangeUserNameRequest request)
  {
    var userId = new UserId(request.Id);
    var user = await _userRepository.GetByIdAsync(userId);

    if (user == null) throw new NotFoundException($"Usuario con ID {request.Id} no encontrado.");

    user.ChangeName(new UserName(request.Name));

    await _userRepository.ChangeNameAsync(user);

    await _unitOfWork.SaveChangesAsync();
  }
}