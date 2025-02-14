namespace backend.Models;

public class AccountBalance
{
    public int Id { get; set; }
    public int PortfolioId { get; set; }
    public decimal Balance { get; set; } 

    public Portfolio Portfolio { get; set; }
}