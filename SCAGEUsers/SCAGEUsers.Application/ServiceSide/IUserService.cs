using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.Application.ServiceSide
{
    public interface IUserService
    {
        public Task<Guid> CreateUser(UserCreateDto request);
        public Task<List<UsersDto>?> GetAllUsers();
        public Task<UsersDto> GetUserById(Guid id);
        public Task<List<UsersDto>> GetUsersByName(string name);
        public Task<Guid> UpdateUser(UserUpdateDto request);
    }
}
