using KidOfTheDayAPI.Dtos;
using KidOfTheDayAPI.Interfaces;
using KidOfTheDayAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace KidOfTheDayAPI.Repositories
{
    public class ResponsibilitiesRepository : IResponsibiltiesRepository
    {
        private readonly IConfiguration _config;
        private SqlDataReader reader;
        public ResponsibilitiesRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task AddResponsibility(ResponsibilityDto responsibilityDto)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            SqlConnection connection = new SqlConnection(connectionString);

            using (var conn = connection)
            using (var command = new SqlCommand("SpAddResponsibility", conn))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@KidId", responsibilityDto.KidId);
                command.Parameters.AddWithValue("@Title", responsibilityDto.Title);
                command.Parameters.AddWithValue("@Description", responsibilityDto.Description);
                command.Parameters.AddWithValue("@Completed", responsibilityDto.Completed);

                await conn.OpenAsync();


                await command.ExecuteNonQueryAsync();

                await conn.CloseAsync();
            }
        }

        public async Task DeleteResponsibility(int id)
        {
            var connectionString = _config.GetConnectionString("DefaultConnection");

            string query = $"DELETE FROM ListOfResponsibilities WHERE Id = {id}";

            using (var conn = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, conn);

                await conn.OpenAsync();

                await command.ExecuteNonQueryAsync();

                await conn.CloseAsync();
            }
        }

        public async Task<List<Responsibility>> GetKidsResponsibilities(int kidId)
        {
            List<Responsibility> listOfResponsibilties = new List<Responsibility>();

            var connectionString = _config.GetConnectionString("DefaultConnection");
            var query = $"SELECT Id, KidId, Title, Description, Completed " +
                $"FROM ListOfResponsibilities WHERE KidId = {kidId}";

            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);


                await connection.OpenAsync();

                reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = (int)reader["Id"];
                        var KidId = (int)reader["KidId"];
                        var title = reader["Title"].ToString();
                        var description = reader["Description"].ToString();
                        bool completed = (bool)reader["Completed"];

                        if (string.IsNullOrEmpty(title))
                        {
                            title = "";
                        }
                        if (string.IsNullOrEmpty(description))
                        {
                            description = "";
                        }

                        Responsibility responsibility = new Responsibility(id, KidId, title, description, completed);
                        listOfResponsibilties.Add(responsibility);
                    }
                }
                await connection.CloseAsync();
            }
            return listOfResponsibilties;
        }
    }
}
