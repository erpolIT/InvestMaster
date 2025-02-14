namespace backend.Models;

public class Investment
{
    public int Id { get; set; }
    public int PortfolioId { get; set; }
    public int AssetId { get; set; }
    // Relacje
    public Portfolio Portfolio { get; set; }
    public Asset Asset { get; set; }
    public List<Transaction> Transactions { get; set; } = new();

}