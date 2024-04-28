using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using AcmeCorpAPI.Domain;

public class CustomerTests
{
    [Fact]
    public void TestCustomerName()
    {
        // Arrange
        var customer = new Customer 
        { 
            Id = 1,
            Name = "John Doe",
            ContactInfo = new ContactInfo { Email = "johndoe@example.com", PhoneNumber = "123-456-7890" },
        };
        
        // Act & Assert
        Assert.Equal("John Doe", customer.Name);
    }
    
    [Fact]
    public void TestCustomerContactInfo()
    {
        // Arrange
        var customer = new Customer 
        { 
            Id = 1,
            Name = "John Doe",
            ContactInfo = new ContactInfo { Email = "johndoe@example.com", PhoneNumber = "123-456-7890" },
        };
        
        // Act & Assert
        Assert.Equal("johndoe@example.com", customer.ContactInfo.Email);
        Assert.Equal("123-456-7890", customer.ContactInfo.PhoneNumber);
    }
}