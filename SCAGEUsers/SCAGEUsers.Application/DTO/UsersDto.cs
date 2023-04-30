
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.DTO
{
    public class UsersDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }

        public UsersDto(string name, string email, Sex sex)
        {
            Name = name;
            Email = email;
            Sex = sex;
        }
    }
}
