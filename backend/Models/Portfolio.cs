using backend.Database;

namespace backend.Models;

public class Portfolio
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public AccountBalance AccountBalance { get; set; }
    public User User { get; set; }
    public ICollection<Investment> Investments { get; set; }
    public ICollection<PortfolioValue> PortfolioValues { get; set; }
}