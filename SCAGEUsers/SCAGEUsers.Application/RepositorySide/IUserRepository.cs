
using SCAGEUsers.Application.AggregateRoot;

namespace SCAGEUsers.Application.RepositorySide
{
    public interface IUserRepository
    {
        public Task<Guid> CreateUser(User user);
        public Task<User> GetUserById(Guid id);
        public Task<User?> GetUserByNameOrEmail(string name, string email);
        public Task<Guid> UpdateUser(User userExist);
    }
}
