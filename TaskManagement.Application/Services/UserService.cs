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
    }
}
