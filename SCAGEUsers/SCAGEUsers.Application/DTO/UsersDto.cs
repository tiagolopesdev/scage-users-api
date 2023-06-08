
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.DTO
{
    public class UsersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }

        public UsersDto(Guid id, string name, string email, Sex sex)
        {
            Id = id;
            Name = name;
            Email = email;
            Sex = sex;
        }
    }
}
