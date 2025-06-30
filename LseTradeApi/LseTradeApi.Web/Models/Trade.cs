using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LseTradeApi.Web.Models
{
    public class Trade
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, Column(TypeName = "varchar(10)")]
        public string TickerSymbol { get; set; } = string.Empty;

        [Required, Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required, Column(TypeName = "decimal(18,4)")]
        public decimal Quantity { get; set; }

        [Required]
        public Guid BrokerId { get; set; }

        [Required]
        public DateTime TradedAt { get; set; } = DateTime.UtcNow;
    }
}