using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.QuerySide;
using SCAGEUsers.Application.RepositorySide;
using SCAGEUsers.Application.ServiceSide;
using SCAGEUsers.Application.VO;

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
            var userExist = await _userRepository.GetUserByNameOrEmail(request.Name, request.Email);

            if (userExist != null) throw new ArgumentException("Usuário com nome " 
                + request.Name +" ou email "+ request.Email +" já existe");

            var user = User.New(Guid.NewGuid(), request.Name, request.Email, request.Sex);

            var response = await _userRepository.CreateUser(user);

            return response;
        }

        public async Task<UsersDto> GetUserById(Guid id)
        {
            return await _userQuery.GetUserById(id);
        }

        public async Task<Guid> UpdateUser(UserUpdateDto request)
        {
            var userExist = await _userRepository.GetUserById(request.Id) ?? throw new ArgumentException("Usuário não encontrado para atualizar");
            
            var userByNameOrEmailExist = await _userRepository.GetUserByNameOrEmail(request.Name, request.Email, userExist.Id);

            if (userByNameOrEmailExist != null) throw new ArgumentException("Usuário com nome "
                + request.Name + " ou email " + request.Email + " já existe");

            userExist.Update(request.Name, request.Email, request.Sex, request.IsEnable);

            return await _userRepository.UpdateUser(userExist);
        }

        public async Task<List<UsersDto>?> GetAllUsers()
        {
            return await _userQuery.GetAllUsers();
        }

        public async Task<List<UsersDto>> GetUsersByFilters(string name, Sex? sex)
        {
            return await _userQuery.GetUsersByFilters(name, sex);
        }
    }
}
