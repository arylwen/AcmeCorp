using Microsoft.EntityFrameworkCore;
using Xunit;
using AcmeCorpAPI.Models;

namespace AcmeCorpAPI.Tests
{

    public class CustomerDtoTests
    {
        [Fact]
        public void TestCustomerDTO()
        {
            // Arrange
            var customer = new CustomerDTO
            {
                Id = 1,
                Name = "John Doe",
                ContactInfo = new ContactInfoDTO
                {
                    Email = "john@doe.com",
                    PhoneNumber = "1234567890"
                }
            };

            // Act
            var resultName = customer.Name;
            var resultEmail = customer.ContactInfo.Email;
            var resultPhoneNumber = customer.ContactInfo.PhoneNumber;

            // Assert
            Assert.Equal("John Doe", resultName);
            Assert.Equal("john@doe.com", resultEmail);
            Assert.Equal("1234567890", resultPhoneNumber);
        }
    }
}