using DealmateApi.Controllers;
using DealmateApi.Domain.Aggregates;
using DealmateApi.Infrastructure.Interfaces;
using DealmateApi.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DealmateUnitTest;

public class BranchControllerTests
{
    private readonly Mock<IRepository<Branch>> _repository;
    private readonly Mock<IBranchRepository> _branchRepository;
    private readonly BranchController _controller;

    public BranchControllerTests()
    {
        _repository = new Mock<IRepository<Branch>>();
        _branchRepository = new Mock<IBranchRepository>();
        _controller = new BranchController(_branchRepository.Object, _repository.Object);
    }

    private static Dealer TestDealer =>
        new()
        {
            Id = 1,
            Name = "TestDealer",
            Address = "ABC"
        };


    private static Branch TestBranch =>
        new()
        {
            Id = 1,
            Name = "TestBranch",
            DealerId = TestDealer.Id
        };

    [Fact]
    public async Task Create_ReturnsOkResult()
    {
        // Arrange
        var branch = TestBranch;
        _branchRepository.Setup(repo => repo.Create(branch)).ReturnsAsync(branch);

        // Act
        var result = await _controller.Create(branch);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Branch>(okResult.Value);
        Assert.Equal(branch.DealerId, returnValue.DealerId);
    }

    [Fact]
    public async Task Update_ReturnsOkResult()
    {
        // Arrange
        var branch = TestBranch;
        branch.Name = "TestBranch1";
        _branchRepository.Setup(repo => repo.Update(branch)).ReturnsAsync(branch);

        // Act 
        var result = await _controller.Update(branch);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Branch>(okResult.Value);
        Assert.Equal(branch.Name, returnValue.Name);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult()
    {
        var branch = TestBranch;
        // Arrange
        _branchRepository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(branch);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
    }
}
