
using System.Reflection.Metadata.Ecma335;

namespace SCAGEUsers.Application.AggregateRoot
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ModifiedBy { get; set; }

        public static User New(Guid id, string name, string email, bool isEnable = true)
        {
            var user = new User
            {
                Id = id,
                Name = name,
                Email = email,
                IsEnable = isEnable,
                CreatedOn = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
            }; 
            return user;
        }
    }
}
