using FluentAssertions;
using LseTradeApi.Web.DTOs;
using LseTradeApi.Web.Interfaces;
using LseTradeApi.Web.Models;
using LseTradeApi.Web.Services;
using NSubstitute;

namespace LseTradeApi.Tests;

[TestClass]
public sealed class Test1
{
    private ITradeRepository _repository;
    private TradeService _sut;

    [TestInitialize]
    public void InitialzeTest()
    {
        _repository = Substitute.For<ITradeRepository>();
        _sut = new TradeService(_repository);
    }

    [DataRow(0)]
    [TestMethod]
    public void ConstructorNullArgument_ThrowsException(int nullParam)
    {
        //Arrange
        var parameters = new object[] {
            _repository
        };
        parameters[nullParam] = null;

        //Assert
        Assert.ThrowsException<ArgumentNullException>(() => new TradeService(
            (ITradeRepository)parameters[0]
        ));
    }

    [TestMethod]
    public async Task RecordTradeAsync_ShouldReturnTradeId()
    {
        // Arrange
        var request = new TradeRequest("LSE", 101.0m, 50.5m, Guid.NewGuid());

        // Act
        var result = await _sut.RecordTradeAsync(request);

        // Assert
        await _repository.Received(1).AddTradeAsync(Arg.Any<Trade>());
        result.Should().NotBeEmpty();
    }

    [TestMethod]
    public async Task GetStockAverageAsync_ShouldReturnCorrectAverage()
    {
        //Arrange
        var trades = new List<Trade>
        {
            new() { TickerSymbol = "LSE", Price = 100, Quantity = 10 },
            new() { TickerSymbol = "LSE", Price = 200, Quantity = 20 }
        };

        _repository.GetTradesBySymbolAsync("LSE").Returns(trades);

        //Act
        var result = await _sut.GetStockAverageAsync("LSE");

        //Assert
        result.Should().NotBeNull();
        result.AveragePrice.Should().Be(150);
    }
}
