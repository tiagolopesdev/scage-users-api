using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;

namespace SCAGEUsers.Application.Extension
{
    public static class UserExtension
    {
        public static UsersDto ToDto(this User user)
        {
            var usersDto = new UsersDto(user.Id, user.Name, user.Email, user.Sex);

            return usersDto;
        }
        public static List<UsersDto> ToDtoList(this IEnumerable<User> user)
        {
            var userOrder = user.OrderByDescending(item => item.CreatedOn.Date);

            var listReturn = new List<UsersDto>();

            foreach (var item in userOrder)
            {
                listReturn.Add(new UsersDto(item.Id, item.Name, item.Email, item.Sex));
            }
            return listReturn;
        }
    }
}
