using LseTradeApi.Web.DTOs;
using LseTradeApi.Web.Interfaces;
using LseTradeApi.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LseTradeApi.Web.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly TradeDbContext _db;

        public TradeRepository(TradeDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task AddTradeAsync(Trade trade)
        {
            _db.Trades.Add(trade);
            await _db.SaveChangesAsync();
        }

        public Task<List<Trade>> GetTradesBySymbolAsync(string symbol) =>
            _db.Trades.Where(t => t.TickerSymbol == symbol).ToListAsync();

        public Task<List<StockAverage>> GetAllStockAveragesAsync() =>
            _db.Trades
              .GroupBy(t => t.TickerSymbol)
              .Select(g => new StockAverage(g.Key, g.Average(t => t.Price)))
              .ToListAsync();

        public Task<List<StockAverage>> GetStockAveragesBySymbolsAsync(List<string> symbols) =>
            _db.Trades
              .Where(t => symbols.Contains(t.TickerSymbol))
              .GroupBy(t => t.TickerSymbol)
              .Select(g => new StockAverage(g.Key, g.Average(t => t.Price)))
              .ToListAsync();
    }
}