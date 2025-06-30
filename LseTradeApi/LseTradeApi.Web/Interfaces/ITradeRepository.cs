using LseTradeApi.Web.DTOs;
using LseTradeApi.Web.Models;

namespace LseTradeApi.Web.Interfaces
{
    public interface ITradeRepository
    {
        Task AddTradeAsync(Trade trade);
        Task<List<Trade>> GetTradesBySymbolAsync(string symbol);
        Task<List<StockAverage>> GetAllStockAveragesAsync();
        Task<List<StockAverage>> GetStockAveragesBySymbolsAsync(List<string> symbols);
    }
}