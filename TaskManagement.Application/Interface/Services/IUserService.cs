using TaskManagement.Common.Dtos.User;

namespace TaskManagement.Application.Interface.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(UserCreate addressCreate);
    Task<IEnumerable<UserGet>> GetUsersAsync();
    Task<UserGet> GetUserAsync(Guid id);
    Task UpdateUserAsync(Guid id, UserUpdate userUpdate);
    Task DeleteUserAsync(Guid id);
}
