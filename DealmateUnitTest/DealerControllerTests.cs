using DealmateApi.Controllers;
using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DealmateUnitTest;

public class DealerControllerTests
{
    private readonly Mock<IRepository<Dealer>> _repository;
    private readonly Mock<IDealerRepository> _dealerRepository;
    private readonly DealerController _controller;

    public DealerControllerTests()
    {
        _repository = new Mock<IRepository<Dealer>>();
        _dealerRepository = new Mock<IDealerRepository>();
        _controller = new DealerController(_dealerRepository.Object, _repository.Object);
    }

    private static Dealer TestDealer =>
        new()
        {
            Id = 1,
            Name = "TestDealer",
            Address = "ABC"
        };

    [Fact]
    public async Task Create_ReturnsOkResult()
    {
        // Arrange
        var dealer = TestDealer;
        _dealerRepository.Setup(repo => repo.Create(dealer)).ReturnsAsync(dealer);

        // Act
        var result = await _controller.Create(dealer);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Dealer>(okResult.Value);
        Assert.Equal(dealer.Name, returnValue.Name);
    }

    [Fact]
    public async Task Update_ReturnsOkResult()
    {
        // Arrange
        var dealer = TestDealer;
        dealer.Address = "IJK";
        _dealerRepository.Setup(repo => repo.Update(dealer)).ReturnsAsync(dealer);

        // Act 
        var result = await _controller.Update(dealer);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Dealer>(okResult.Value);
        Assert.Equal("IJK", returnValue.Address);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult()
    {
        var dealer = TestDealer;
        // Arrange
        _dealerRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(dealer);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }
}
