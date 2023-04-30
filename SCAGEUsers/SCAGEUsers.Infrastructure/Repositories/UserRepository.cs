using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.RepositorySide;

namespace SCAGEUsers.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("DefaultConnection"); } }

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Guid> CreateUser(User user)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                int entity;
                try
                {
                    entity = await connection.ExecuteAsync(
                        "INSERT INTO users(" +
                            "id, " +
                            "name, " +
                            "email, " +
                            "sex, " +
                            "createdOn, " +
                            "createdBy) " +
                        "VALUES(" +
                            "@id, " +
                            "@name, " +
                            "@email, " +
                            "@sex, " +
                            "@createdOn, " +
                            "@createdBy);",
                        new
                        {
                            id = user.Id,
                            name = user.Name,
                            email = user.Email,
                            sex = user.Sex.ToString(),
                            createdOn = user.CreatedOn,
                            createdBy = user.CreatedBy
                        });
                    
                    return entity != 0 ? user.Id : Guid.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
