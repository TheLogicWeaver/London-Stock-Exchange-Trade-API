using LseTradeApi.Web.DTOs;
using LseTradeApi.Web.Interfaces;
using LseTradeApi.Web.Models;

namespace LseTradeApi.Web.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _repository;

        public TradeService(ITradeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Guid> RecordTradeAsync(TradeRequest request)
        {
            var trade = new Trade
            {
                TickerSymbol = request.TickerSymbol.ToUpperInvariant(),
                Price = request.Price,
                Quantity = request.Quantity,
                BrokerId = request.BrokerId
            };

            await _repository.AddTradeAsync(trade);
            return trade.Id;
        }

        public async Task<StockAverage?> GetStockAverageAsync(string symbol)
        {
            var trades = await _repository.GetTradesBySymbolAsync(symbol.ToUpperInvariant());
            if (trades.Count == 0) return null;
            return new StockAverage(symbol.ToUpperInvariant(), trades.Average(t => t.Price));
        }

        public Task<List<StockAverage>> GetAllAveragesAsync() => _repository.GetAllStockAveragesAsync();

        public Task<List<StockAverage>> GetAveragesBySymbolsAsync(List<string> symbols) =>
            _repository.GetStockAveragesBySymbolsAsync(symbols.Select(s => s.ToUpperInvariant()).ToList());
    }
}