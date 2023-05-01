using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.ServiceSide
{
    public interface IUserService
    {
        public Task<Guid> CreateUser(UserCreateDto request);
        public Task<List<UsersDto>?> GetAllUsers();
        public Task<UsersDto> GetUserById(Guid id);
        public Task<List<UsersDto>> GetUsersByFilters(string name, Sex? sex);
        public Task<Guid> UpdateUser(UserUpdateDto request);
    }
}
