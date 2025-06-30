using LseTradeApi.Web.DTOs;

namespace LseTradeApi.Web.Interfaces
{
    public interface ITradeService
    {
        Task<Guid> RecordTradeAsync(TradeRequest request);
        Task<StockAverage?> GetStockAverageAsync(string symbol);
        Task<List<StockAverage>> GetAllAveragesAsync();
        Task<List<StockAverage>> GetAveragesBySymbolsAsync(List<string> symbols);
    }
}