using apbd_tutorial8.Model;
using apbd_tutorial8.Model.DTO;
using Microsoft.Data.SqlClient;

namespace apbd_tutorial8.Repository;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString="Data Source=localhost, 1433; User=SA; Password=yourStrong(!)Password; " +
                                              "Initial Catalog=apbd; Integrated Security=False;Connect " +
                                              "Timeout=30;Encrypt=False;Trust Server Certificate=False";
    
    public async Task<Trip_ClientDTO> GetTripByClientIdAsync(int id)
    {
        Trip_ClientDTO client=null;
        string command="SELECT * FROM Client_Trip ct JOIN Trip t ON ct.IdTrip=t.IdTrip WHERE ct.IdClient = @id";
        
        using(SqlConnection conn=new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", id);
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    int idClient = reader.GetInt32(reader.GetOrdinal("IdClient"));
                    if (client == null)
                    {
                        client = new Trip_ClientDTO{
                            IdClient = idClient,
                        };
                    }
                    
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
        return client;
    }

    public async Task AddClientAsync(Client client)
    {
        throw new NotImplementedException();
    }
}