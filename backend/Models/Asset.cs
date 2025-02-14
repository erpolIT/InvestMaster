namespace backend.Models;

public class Asset
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public int AssetTypeId { get; set; } 
    public string Currency { get; set; }
    public decimal CurrentPriceOpen { get; set; }
    public decimal CurrentPriceClose { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    public AssetType AssetType { get; set; }
    public ICollection<Investment> Investments { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}