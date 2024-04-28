using Microsoft.EntityFrameworkCore;
using Xunit;
using AcmeCorpAPI.Domain;

namespace AcmeCorpAPI.Tests
{
    public class OrderTests
    {
        [Fact]
        public void TestOrderId()
        {
            // Arrange
            var order = new Order();

            // Act
            order.Id = 123;

            // Assert
            Assert.Equal(123, order.Id);
        }

        [Fact]
        public void TestOrderDetails()
        {
            // Arrange
            var order = new Order();

            // Act
            order.Details = "Sample details";

            // Assert
            Assert.Equal("Sample details", order.Details);
        }

        [Fact]
        public void TestOrderDate()
        {
            // Arrange
            var order = new Order();

            // Act
            order.OrderDate = new DateTime(2019, 8, 1);

            // Assert
            Assert.Equal(new DateTime(2019, 8, 1), order.OrderDate);
        }

        [Fact]
        public void TestCustomerId()
        {
            // Arrange
            var order = new Order();

            // Act
            order.CustomerId = 456;

            // Assert
            Assert.Equal(456, order.CustomerId);
        }

    }
}