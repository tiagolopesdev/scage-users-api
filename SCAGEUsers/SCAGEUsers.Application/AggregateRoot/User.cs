
using SCAGEUsers.Application.VO;
using System.Reflection.Metadata.Ecma335;

namespace SCAGEUsers.Application.AggregateRoot
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid ModifiedBy { get; set; }

        public static User New(Guid id, string name, string email, Sex sex, bool isEnable = true)
        {
            var user = new User
            {
                Id = id,
                Name = name,
                Email = email,
                Sex = sex,
                IsEnable = isEnable,
                CreatedOn = DateTime.Now,
                CreatedBy = Guid.NewGuid(),
            };
            return user;
        }

        public void Update(string name, string email, Sex sex, bool isEnable)
        {
            Name = name;
            Email = email;
            Sex = sex;
            IsEnable = isEnable;
        }
    }
}
