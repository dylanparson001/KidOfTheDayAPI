using KidOfTheDayAPI.Controllers;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace KidOfTheDayAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private SqlDataReader reader;

        public UserRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            User user = new User();
            var connectionString = _config.GetConnectionString("DefaultConnection");

            //if (connectionString!= null)
            //{
            SqlConnection connection = new SqlConnection(connectionString);
            //}
            using (var conn = connection)
            using (var command = new SqlCommand("[GetUserByUsernameAndPassword]"))
            {
                await conn.OpenAsync();

                command.Parameters.AddWithValue("userName", username);
                command.Parameters.AddWithValue("password", password);

                reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        var userName = reader["Username"].ToString();
                        var passwordSalt = reader["PasswordSalt"].ToString();
                        var passwordHash = reader["PasswordHash"].ToString();
                        var firstName = reader["FirstName"].ToString();
                        var lastName = reader["LastName"].ToString();
                        var emailAddress = reader["EmailAddress"].ToString();

                        user = new User(
                                id: id,
                                username: userName,
                                passwordHash: passwordHash,
                                firstName: firstName,
                                lastName: lastName,
                                emailAddress: emailAddress
                            );
                    }
                }

                conn.Close();
            }

            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            User user = null;
            var connectionString = _config.GetConnectionString("DefaultConnection");
            var query = "SELECT * FROM Users WHERE Username = @Username";
           
           
            SqlConnection connection = new SqlConnection(connectionString);
           
            using (var conn = connection)
            {
                conn.Open();
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@Username", username);

                reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int id = (int)reader["Id"];
                        var userName = reader["Username"].ToString();
                        var passwordHash = reader["PasswordHash"].ToString();
                        var firstName = reader["FirstName"].ToString();
                        var lastName = reader["LastName"].ToString();
                        var emailAddress = reader["EmailAddress"].ToString();

                        user = new User (
                                id: id,
                                username: userName,
                                passwordHash: passwordHash,
                                firstName: firstName,
                                lastName: lastName,
                                emailAddress: emailAddress
                            );
                    }
                }

                conn.Close();
            }

            return user;
        }

        public async Task RegisterUser(string username, string password, string email, string firstName, string lastName)
        {
            //User user = null;
            var connectionString = _config.GetConnectionString("DefaultConnection");
            var query = "SELECT * FROM Users WHERE Username = @Username";

            
            SqlConnection connection = new SqlConnection(connectionString);

            using (var conn = connection)
            using (var command = new SqlCommand("SpRegisterUser", conn))
            {

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", password);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);

                await conn.OpenAsync();

                reader = await command.ExecuteReaderAsync();

             

                await conn.CloseAsync();
            }

            //return user;
        }
    }
}
