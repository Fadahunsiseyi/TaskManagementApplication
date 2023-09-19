using AutoMapper;
using FluentValidation;
using TaskManagement.Application.Validation;
using TaskManagement.Common.Dtos.User;
using TaskManagement.Domain.Entities;
using TaskManagement.Application.Interface.Persistence;
using TaskManagement.Application.Interface.Services;

namespace TaskManagement.Application.Services;

public class UserService : IUserService
{
    private IMapper Mapper { get; }
    private IGenericRepository<User> UserRepository { get; }
    private UserCreateValidator UserCreateValidator { get; }
    private UserUpdateValidator UserUpdateValidator { get; }

    public UserService(IMapper mapper, IGenericRepository<User> userRepository, UserCreateValidator userCreateValidator, UserUpdateValidator userUpdateValidator)
    {
        Mapper = mapper;
        UserRepository = userRepository;
        UserCreateValidator = userCreateValidator;
        UserUpdateValidator = userUpdateValidator;
    }



    public async Task<Guid> CreateUserAsync(UserCreate addressCreate)
    {
        await UserCreateValidator.ValidateAndThrowAsync(addressCreate);

        var entity = Mapper.Map<User>(addressCreate);
        await UserRepository.InsertAsync(entity);
        await UserRepository.SaveChangesAsync();
        return entity.Id;
    }
    public async Task<IEnumerable<UserGet>> GetUsersAsync()
    {
        var entity = await UserRepository.GetAllAsync(null,null);
        return Mapper.Map<IEnumerable<UserGet>>(entity);
    }
    public async Task<UserGet> GetUserAsync(Guid id)
    {
        if(!await UserRepository.ExistsAsync(id)) throw new Exception("User not found");
        var entity = await UserRepository.GetByIdAsync(id);
        return Mapper.Map<UserGet>(entity);
    }
    public async System.Threading.Tasks.Task UpdateUserAsync(Guid id, UserUpdate userUpdate)
    {
        if (!await UserRepository.ExistsAsync(id)) throw new Exception("User not found");
        await UserUpdateValidator.ValidateAndThrowAsync(userUpdate);

        var existingEntity = await UserRepository.GetByIdAsync(id);
        var entity = Mapper.Map(userUpdate, existingEntity);
        UserRepository.Update(entity);
        await UserRepository.SaveChangesAsync();
    }
    public async System.Threading.Tasks.Task DeleteUserAsync(Guid id)
    {
        if (!await UserRepository.ExistsAsync(id)) throw new Exception("User not found");
        var entity = await UserRepository.GetByIdAsync(id);
        UserRepository.Delete(entity);
        await UserRepository.SaveChangesAsync();
    }
}
