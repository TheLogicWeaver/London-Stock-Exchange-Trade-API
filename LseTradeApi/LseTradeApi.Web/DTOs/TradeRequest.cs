namespace LseTradeApi.Web.DTOs
{
    public record TradeRequest(string TickerSymbol, decimal Price, decimal Quantity, Guid BrokerId);
}