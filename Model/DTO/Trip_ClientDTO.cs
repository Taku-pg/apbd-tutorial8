namespace apbd_tutorial8.Model.DTO;

public class Trip_ClientDTO 
{
    public int IdClient { get; set; }
    public List<Client_Trip> Trips { get; set; } = new List<Client_Trip>();
}

public class Client_Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public int RegisteredAt { get; set; }
    public int? PaymentDate { get; set; }
}