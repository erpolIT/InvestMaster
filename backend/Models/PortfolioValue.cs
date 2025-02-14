namespace backend.Models;

public class PortfolioValue
{
    public int Id { get; set; }
    public int PortfolioId { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    public Portfolio Portfolio { get; set; }
}