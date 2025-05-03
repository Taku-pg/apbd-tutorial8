using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial8.Model;

public class Client
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Telephone { get; set; }
    public string Pesel { get; set; }
}