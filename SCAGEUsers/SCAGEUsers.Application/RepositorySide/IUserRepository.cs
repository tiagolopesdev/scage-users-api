
using SCAGEUsers.Application.AggregateRoot;

namespace SCAGEUsers.Application.RepositorySide
{
    public interface IUserRepository
    {
        public Task<Guid> CreateUser(User user);
    }
}
