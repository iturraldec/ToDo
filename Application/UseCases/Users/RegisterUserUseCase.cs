using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using Domain.Interfaces;

namespace Application.UseCases.Users;

public class RegisterUserUseCase
{
  private readonly IRepository<UserEntity, UserId> _repository;
  private readonly IUserReads _userReadService;
  private readonly IUnitOfWork _unitOfWork;

  public RegisterUserUseCase(IRepository<UserEntity, UserId> repository, IUserReads userReadService, IUnitOfWork unitOfWork)
  {
    _repository = repository;
    _userReadService = userReadService;
    _unitOfWork = unitOfWork;
  }

  public async Task<string> Execute(RegisterUserRequest request)
  {
    if (await _userReadService.IsEmailExistsAsync(request.Email)) throw new AlreadyExistsException($"El email {request.Email} ya está registrado.");
    
    var entity = UserEntity.Create(UserId.Create(), new UserRole((UserRolesEnum)request.Role), new UserName(request.Name), new UserEmail(request.Email));

    await _repository.RegisterAsync(entity);
    await _unitOfWork.SaveChangesAsync();

    return entity.Id.ToString();
  }
}