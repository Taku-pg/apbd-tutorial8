using apbd_tutorial8.Model.DTO;
using Microsoft.Data.SqlClient;

namespace apbd_tutorial8.Repository;

public class Trip_CountryRepository : ITrip_CountryRepository
{
    private readonly string _connectionString="Data Source=localhost, 1433; User=SA; Password=yourStrong(!)Password; " +
                                             "Initial Catalog=apbd; Integrated Security=False;Connect " +
                                             "Timeout=30;Encrypt=False;Trust Server Certificate=False";

    public async Task<List<Trip_CountryDTO>> getTripsAsync()
    {
        var trips = new List<Trip_CountryDTO>();
        string command = "SELECT * FROM Trip"+ 
            "JOIN Country_Trip ON Trip.Id=Country_Trip.IdTrip"+
            "JOIN Country on Country.Id=Country.IdCountry";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(command, conn))
        {
            await conn.OpenAsync();
            using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    int idTrip=reader.GetOrdinal("IdTrip");
                    string nameTrip = reader.GetString(reader.GetOrdinal("Name"));
                    string description = reader.GetString(reader.GetOrdinal("Description"));
                    DateTime dateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom"));
                    DateTime dateTo = reader.GetDateTime(reader.GetOrdinal("DateTo"));
                    int max=reader.GetInt32(reader.GetOrdinal("MaxPeople"));
                    int idCountry=reader.GetOrdinal("IdCountry");
                    string nameCountry = reader.GetString(reader.GetOrdinal("Name1"));
                    
                    var trip=trips.FirstOrDefault(t => t.IdTrip == idTrip);
                    if (trip == null)
                    {
                        trip=new Trip_CountryDTO
                        {
                            IdTrip = idTrip,
                            Name=nameTrip,
                            Description=description,
                            DateFrom=dateFrom,
                            DateTo=dateTo,
                            MaxPeople=max,
                        };
                    }
                    trip.Countries.Add(new Country{IdCountry = idCountry, Name = nameCountry});
                }
            }
        }
        return trips;
    }
}