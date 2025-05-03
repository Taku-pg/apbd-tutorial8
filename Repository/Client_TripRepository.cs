using apbd_tutorial8.Model;
using apbd_tutorial8.Model.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace apbd_tutorial8.Repository;

public class Client_TripRepository : IClient_TripRepository
{
    private readonly IConfiguration _configuration;

    public Client_TripRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        Trip? trip = null;
        string query="SELECT * FROM Trip WHERE IdTrip = @id;";
        
        using (SqlConnection conn = new(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            SqlDataReader reader =await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                trip = new Trip
                            {
                                IdTrip = reader.GetInt32(reader.GetOrdinal("IdTrip")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                                DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                MaxPeople = reader.GetInt32(reader.GetOrdinal("MaxPeople")),
                            };
            }
        }
        return trip;
    }

    public async Task<Client?> GetClientByIdAsync(int id)
    {
        Client? client = null;
        string query="SELECT * FROM Client WHERE IdClient = @id;";
        
        using (SqlConnection conn = new(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            SqlDataReader reader =await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                client = new Client
                            {
                                IdClient = reader.GetInt32(reader.GetOrdinal("IdClient")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Telephone = reader.GetString(reader.GetOrdinal("Telephone")),
                                Pesel = reader.GetString(reader.GetOrdinal("Pesel")),
                            };
            }
            
        }
        return client;
    }

    public async Task<int> GetNumberOfParticipantsAsync(int tripId)
    {
        string query = "SELECT COUNT(*) FROM Client_Trip WHERE IdTrip = @id;";

        using (SqlConnection conn = new(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@id", tripId);
            await conn.OpenAsync();
            return (int)(await cmd.ExecuteScalarAsync());
        }
    }
    

    public async Task<bool> AddTripAsync(int clientId, int tripId)
    {
        string query=@"INSERT INTO Client_Trip (IdClient, IdTrip,RegisteredAt) 
            VALUES (@clientId, @tripId,@registeredAt);";
        
        using (SqlConnection conn = new(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("@clientId", clientId);
            cmd.Parameters.AddWithValue("@tripId", tripId);
            string nowStr=DateTime.Now.ToString("yyyyMMdd");
            cmd.Parameters.AddWithValue("@registeredAt", Convert.ToInt32(nowStr));
            await conn.OpenAsync();
            
            int affected= await cmd.ExecuteNonQueryAsync();
            return affected == 1;
        }
    }
}