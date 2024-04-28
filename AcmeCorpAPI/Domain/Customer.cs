using Microsoft.EntityFrameworkCore;

namespace AcmeCorpAPI.Domain;

public class Customer {
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public ContactInfo ContactInfo { get; set; }
    public List<Order> Orders { get; private set;} = new();
}

[Owned]
public class ContactInfo {
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

}