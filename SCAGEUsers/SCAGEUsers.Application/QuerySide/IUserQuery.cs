
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.QuerySide
{
    public interface IUserQuery
    {
        public Task<List<UsersDto>?> GetAllUsers();
        public Task<UsersDto> GetUserById(Guid id);
        public Task<List<UsersDto>> GetUsersByFilters(string? name, Sex? sex);
    }
}
