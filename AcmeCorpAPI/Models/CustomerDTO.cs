using Microsoft.EntityFrameworkCore;

namespace AcmeCorpAPI.Models;

public class CustomerDTO {
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public ContactInfoDTO ContactInfo { get; set; }
}

[Owned]
public class ContactInfoDTO {
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

}