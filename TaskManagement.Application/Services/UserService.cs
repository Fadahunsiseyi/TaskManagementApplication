using AutoMapper;
using TaskManagement.Domain.Dtos.User;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Interface.Persistence;
using TaskManagement.Domain.Interface.Services;

namespace TaskManagement.Application.Services
{
    public class UserService : IUserService
    {
        private IMapper Mapper { get; }
        private IGenericRepository<User> UserRepository { get; }

        public UserService(IMapper mapper, IGenericRepository<User> userRepository)
        {
            Mapper = mapper;
            UserRepository = userRepository;
        }



        public async Task<Guid> CreateUserAsync(UserCreate addressCreate)
        {
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
            var entity = await UserRepository.GetByIdAsync(id);
            return Mapper.Map<UserGet>(entity);
        }
        public async System.Threading.Tasks.Task UpdateUserAsync(Guid id, UserUpdate userUpdate)
        {
            var existingEntity = await UserRepository.GetByIdAsync(id);
            if (existingEntity is null) throw new Exception("User not found");
            var entity = Mapper.Map(userUpdate, existingEntity);
            UserRepository.Update(entity);
            await UserRepository.SaveChangesAsync();
        }
    }
}
