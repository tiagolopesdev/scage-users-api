
using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using SCAGEUsers.Application.AggregateRoot;
using SCAGEUsers.Application.DTO;
using SCAGEUsers.Application.Extension;
using SCAGEUsers.Application.QuerySide;
using SCAGEUsers.Application.VO;

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
                            "u.id as Id, " +
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
                            "u.id as Id, " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex, " +
                            "u.createdOn as CreatedOn " +
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

        public async Task<List<UsersDto>> GetUsersByFilters(string? name, Sex? sexParam)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    string isExistSex = !string.IsNullOrEmpty(sexParam.ToString()) ? " AND u.sex = @sex" : string.Empty;
                    string isExistName = !string.IsNullOrEmpty(name) ? " AND u.name LIKE @name " : string.Empty;

                    var response = await connection.QueryAsync<User>(
                        "SELECT " +
                            "u.id as Id, " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex, " +
                            "u.createdOn as CreatedOn " +
                        "FROM users as u " +
                        "WHERE isEnable = 1 " +
                            $"{isExistName}" +
                            $"{isExistSex}",
                        new
                        {
                            name = $"%{name}%",
                            sex = sexParam.ToString()
                        });

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
