using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.Application.Extension
{
    public static class UserExtension
    {
        public static UsersDto ToDto(this User user)
        {
            var usersDto = new UsersDto(user.Name, user.Email, user.Sex);

            return usersDto;
        }
    }
}
