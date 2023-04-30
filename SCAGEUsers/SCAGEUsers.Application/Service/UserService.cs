using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.QuerySide;
using SCAGEUsers.Application.RepositorySide;
using SCAGEUsers.Application.ServiceSide;

namespace SCAGEUsers.Application.Service
{
    public class UserService : IUserService
    {
        public IUserQuery _userQuery { get; set; }
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository, IUserQuery userQuery)
        {
            _userQuery = userQuery;
            _userRepository = userRepository;
        }

        public async Task<Guid> CreateUser(UserCreateDto request)
        {
            var user = User.New(Guid.NewGuid(), request.Name, request.Email, request.Sex);

            var response = await _userRepository.CreateUser(user);

            return response;
        }

        public async Task<UsersDto> GetUserById(Guid id)
        {
            return await _userQuery.GetUserById(id);
        }
    }
}
