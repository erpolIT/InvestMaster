namespace backend.Dto;

public class TransactionDto
{
    public int InvestmentId { get; set; }
    public int PortfolioId { get; set; }
    public string Type { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Fee { get; set; }
    public string Notes { get; set; }
}