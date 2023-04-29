
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.Utils;

namespace SCAGEUsers.Application.ServiceSide
{
    public interface IUserService
    {
        public Task<Guid> CreateUser(UserCreateDto request);
    }
}
