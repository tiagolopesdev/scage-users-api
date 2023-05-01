
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.Extension;
using SCAGEUsers.Application.QuerySide;

namespace SCAGEUsers.Infrastructure.Queries
{
    public class UserQueries : IUserQuery
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString { get { return _configuration.GetConnectionString("DefaultConnection"); } }

        public UserQueries(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<UsersDto> GetUserById(Guid id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<User>(
                        "SELECT " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 AND u.id = @id;", new { id });

                    if (response.Count() == 0) return null;

                    return response.ToList().FirstOrDefault().ToDto();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<UsersDto>?> GetAllUsers()
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<User>(
                        "SELECT " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE isEnable = 1;");

                    if (response.Count() == 0) return null;

                    return response.ToList().ToDtoList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<List<UsersDto>> GetUsersByName(string name)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.QueryAsync<User>(
                        "SELECT " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 AND u.name LIKE @name;",
                        new { name = $"%{name}%" });

                    return response.ToList().ToDtoList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
