using TaskManagement.Domain.Dtos.User;

namespace TaskManagement.Domain.Interface.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(UserCreate addressCreate);
    Task<IEnumerable<UserGet>> GetUsersAsync();
    Task<UserGet> GetUserAsync(Guid id);
    Task UpdateUserAsync(Guid id, UserUpdate userUpdate);
}
