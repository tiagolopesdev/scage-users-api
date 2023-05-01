
using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.Application.QuerySide
{
    public interface IUserQuery
    {
        public Task<List<UsersDto>?> GetAllUsers();
        public Task<UsersDto> GetUserById(Guid id);
        public Task<List<UsersDto>> GetUsersByName(string name);
    }
}
