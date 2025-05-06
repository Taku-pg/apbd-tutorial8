using System.ComponentModel.DataAnnotations;

namespace apbd_tutorial8.Model;

public class Client
{
    public int IdClient { get; set; }
    [MaxLength(120)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Telephone { get; set; }
    [MinLength(120)]
    public string Pesel { get; set; }
}