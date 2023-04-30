
using SCAGEUsers.Application.VO;

namespace SCAGEUsers.Application.DTO
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
        public bool IsEnable { get; set; }
    }
}
