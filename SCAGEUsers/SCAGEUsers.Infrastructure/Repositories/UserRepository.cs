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

        public async Task<User> GetUserById(Guid id)
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

                    return response.ToList().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<Guid> UpdateUser(User userExist)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    var response = await connection.ExecuteAsync(
                        "UPDATE users SET " +
                            "name = @name, " +
                            "email = @email, " +
                            "sex = @sex, " +
                            "modifiedOn = @modifiedOn, " +
                            "modifiedBy = @modifiedBy " +
                        "WHERE id = @id;", 
                        new 
                        { 
                            name = userExist.Name,
                            email = userExist.Email,
                            sex = userExist.Sex.ToString(),
                            modifiedOn = DateTime.Now,
                            modifiedBy = Guid.NewGuid().ToString(),
                            id = userExist.Id
                        });

                    return response == 1 ? userExist.Id : Guid.Empty;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<User?> GetUserByNameOrEmail(string name, string email, Guid? id)
        {
            using (var connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    string conditionId = id != Guid.Empty ? " u.id NOT IN (@idUser) " : string.Empty;

                    var response = await connection.QueryAsync<User>(
                        "SELECT " +
                            "u.id as Id, " +
                            "u.name as Name, " +
                            "u.email as Email, " +
                            "u.sex as Sex " +
                        "FROM users as u " +
                        "WHERE " +
                            "isEnable = 1 AND " +
                            $"{conditionId}" +
                            "AND u.name = (SELECT " +
                                "us.name FROM users as us " +
                                "WHERE us.name = @name OR us.email = @email) ",
                        new 
                        { 
                            name,
                            email,
                            idUser = id
                        });

                    return response.Count() == 0 ? 
                        null :
                        response.ToList().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
