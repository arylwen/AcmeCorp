using AcmeCorpAPI.Domain;
using System;
using Xunit;

public class OrderDTOTests
{
    [Fact]
    public void Id_PropertyGetterAndSetter()
    {
        // Arrange
        var orderDTO = new OrderDTO();
        
        // Act
        orderDTO.Id = 123;
        
        // Assert
        Assert.Equal(123, orderDTO.Id);
    }

    [Fact]
    public void Details_PropertyGetterAndSetter()
    {
        // Arrange
        var orderDTO = new OrderDTO();

        // Act
        orderDTO.Details = "Some details";
        
        // Assert
        Assert.Equal("Some details", orderDTO.Details);
    }

    [Fact]
    public void OrderDate_PropertyGetterAndSetter()
    {
        // Arrange
        var orderDTO = new OrderDTO();
        DateTime dateTimeNow = DateTime.Now;
        
        // Act
        orderDTO.OrderDate = dateTimeNow;
        
        // Assert
        Assert.Equal(dateTimeNow, orderDTO.OrderDate);
    }

    [Fact]
    public void CustomerId_PropertyGetterAndSetter()
    {
        // Arrange
        var orderDTO = new OrderDTO();
        
        // Act
        orderDTO.CustomerId = 456;
        
        // Assert
        Assert.Equal(456, orderDTO.CustomerId);
    }
}