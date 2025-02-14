using Microsoft.EntityFrameworkCore;

namespace backend.Models;

public class Transaction
{
    public int Id { get; set; }
    public int InvestmentId { get; set; }
    public string Type { get; set; } // "Buy" lub "Sell"
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    public decimal Fee { get; set; }
    public string Notes { get; set; }
    
    public Investment Investment { get; set; }
}