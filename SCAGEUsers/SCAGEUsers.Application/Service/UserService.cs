

using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.RepositorySide;
using SCAGEUsers.Application.ServiceSide;

namespace SCAGEUsers.Application.Service
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateUser(UserCreateDto request)
        {
            var user = User.New(Guid.NewGuid(), request.Name, request.Email);

            var response = await _userRepository.CreateUser(user);

            return response;
        }
    }
}
