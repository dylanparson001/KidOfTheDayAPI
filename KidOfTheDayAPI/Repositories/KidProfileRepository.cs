using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KidOfTheDayAPI.Repositories
{
    public class KidProfileRepository : IKidProfileRepository
    {
        private readonly IConfiguration _config;
        private SqlDataReader reader;


        public KidProfileRepository(IConfiguration config)
        {
            _config = config;
        }
        public async Task AddKidProfile(KidProfileDto profile)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);
            using (var conn = connection)
            using (var command = new SqlCommand("SpCreateKidProfile", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                command.Parameters.AddWithValue("@LastName", profile.LastName);
                command.Parameters.AddWithValue("@Schedule", profile.Schedule);
                command.Parameters.AddWithValue("@UserId", profile.UserId);

                await conn.OpenAsync();

                reader =  await command.ExecuteReaderAsync();

                await conn.CloseAsync();
            }
        }

        public async Task<List<KidProfile>> GetKidsByUser(int userId)
        {
            List<KidProfile> profiles = new List<KidProfile>();

            var connectionString = _config.GetConnectionString("DefaultConnection");
            var query = "SELECT Id, UserId, FirstName, LastName, Schedule " +
                $"FROM KidProfiles WHERE UserId = {userId}";

            SqlConnection connection = new SqlConnection(connectionString);
            using (var conn = connection)
            {
                var command = new SqlCommand(query, conn);
                await conn.OpenAsync();

                reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = (int) reader["Id"];
                        var firstName = reader["FirstName"].ToString();
                        var lastName = reader["LastName"].ToString();
                        var schedule = (int) reader["Schedule"];
                        var user = (int)reader["UserId"];

                        var profileToAdd = new KidProfile(
                            id,
                            user,
                            firstName,
                            lastName,
                            schedule
                            );

                        profiles.Add(profileToAdd);

                       
                    }
                }
                await conn.CloseAsync();
            }
            return profiles;
        }
    }
}
