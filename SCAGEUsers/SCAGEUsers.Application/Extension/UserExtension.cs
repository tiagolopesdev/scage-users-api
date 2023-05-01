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
        public static List<UsersDto> ToDtoList(this IEnumerable<User> user)
        {
            var listReturn = new List<UsersDto>();

            foreach (var item in user)
            {
                listReturn.Add(new UsersDto(item.Name, item.Email, item.Sex));
            }
            return listReturn;
        }
    }
}
