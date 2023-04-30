
using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.Application.QuerySide
{
    public interface IUserQuery
    {
        public Task<UsersDto> GetUserById(Guid id);
    }
}
