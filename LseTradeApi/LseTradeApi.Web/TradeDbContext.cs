using LseTradeApi.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace LseTradeApi.Web
{
    public class TradeDbContext(DbContextOptions<TradeDbContext> options) : DbContext(options)
    {
        public DbSet<Trade> Trades => Set<Trade>();
    }
}