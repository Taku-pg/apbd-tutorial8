using apbd_tutorial8.Model;
using apbd_tutorial8.Model.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace apbd_tutorial8.Repository;

public class ClientRepository : IClientRepository
{
    private readonly IConfiguration _configuration;
    public ClientRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<Trip_ClientDTO?> GetTripByClientIdAsync(int id)
    {
        Trip_ClientDTO? client=null;
        string command="SELECT * FROM Client c "+ 
            "LEFT JOIN Client_Trip ct ON c.IdClient=ct.IdClient "+ 
            "LEFT JOIN Trip t ON ct.IdTrip=t.IdTrip WHERE c.IdClient = @id";
        
        using(SqlConnection conn=new SqlConnection(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine("Enter loop");
                    int idClient = reader.GetInt32(reader.GetOrdinal("IdClient"));
                    if (client == null)
                    {
                        client = new Trip_ClientDTO{
                            IdClient = idClient,
                        };
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("IdTrip")))
                    {
                        int idTrip=reader.GetInt32(reader.GetOrdinal("IdTrip")); 
                        string nameTrip = reader.GetString(reader.GetOrdinal("Name"));
                        string description = reader.GetString(reader.GetOrdinal("Description")); 
                        DateTime dateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")); 
                        DateTime dateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")); 
                        int max=reader.GetInt32(reader.GetOrdinal("MaxPeople")); 
                        int registeredAt = reader.GetInt32(reader.GetOrdinal("RegisteredAt"));
                        int? paymentDate = reader.IsDBNull(reader.GetOrdinal("PaymentDate")) 
                            ? null: reader.GetInt32(reader.GetOrdinal("PaymentDate")); 
                        client.Trips.Add(new Client_Trip 
                        { 
                            IdTrip = idTrip, 
                            Name = nameTrip, 
                            Description = description, 
                            DateFrom = dateFrom, 
                            DateTo = dateTo,
                            MaxPeople = max,
                            RegisteredAt = registeredAt, 
                            PaymentDate = paymentDate
                        });                            
                    }
                }
            }
        }
        return client;
    }

    public async Task<int> AddClientAsync(Client client)
    {
        string command=@"INSERT INTO Client(FirstName,LastName,Email,Telephone,Pesel) VALUES (@FirstName,
                           @LastName,@Email,@Telephone,@Pesel); 
                            SELECT SCOPE_IDENTITY();";

        using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("apbd-db")))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync(); 
            cmd.Parameters.AddWithValue("@FirstName", client.FirstName); 
            cmd.Parameters.AddWithValue("@LastName", client.LastName); 
            cmd.Parameters.AddWithValue("@Email", client.Email); 
            cmd.Parameters.AddWithValue("@Telephone", client.Telephone); 
            cmd.Parameters.AddWithValue("@Pesel", client.Pesel); 
            var newId = await cmd.ExecuteScalarAsync(); 
            return Convert.ToInt32(newId);
        }
    }
}