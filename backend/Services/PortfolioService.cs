using backend.Database;
using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class PortfolioService
{
    private readonly ApiDbContext _context;

    public PortfolioService(ApiDbContext context)
    {
        _context = context;
    }
    
    public async Task<Portfolio?> GetPortfolioAsync(string userId)
    {
        return await _context.Portfolios.FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<Portfolio> CreatePortfolioAsync(CreatePortfolioDto dto)
    {
        var newPortfolio = new Portfolio
        {
            UserId = dto.UserId,
            Name = dto.Name,
            Description = "",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Investments = new List<Investment>(),
            PortfolioValues = new List<PortfolioValue>()
        };

        _context.Portfolios.Add(newPortfolio);
        await _context.SaveChangesAsync();

        return newPortfolio;
    }

    public async Task<PortfolioValue?> UpdatePortfolioValueAsync(int portfolioId)
    {
        var investments = await _context.Investments
            .Where(i => i.PortfolioId == portfolioId)
            .ToListAsync();

        if (!investments.Any()) return null;

        decimal totalValue = 0;

        foreach (var investment in investments)
        {
            var transactions = await _context.Transactions
                .Where(t => t.InvestmentId == investment.Id)
                .ToListAsync();

            decimal quantity = transactions
                                   .Where(t => t.Type == "BUY")
                                   .Sum(t => t.Quantity) 
                               - transactions
                                   .Where(t => t.Type == "SELL")
                                   .Sum(t => t.Quantity);

            if (quantity > 0)
            {
                decimal lastPrice = await _context.Assets
                    .Where(a => a.Id == investment.AssetId)
                    .Select(a => a.CurrentPriceClose)
                    .FirstOrDefaultAsync();

                totalValue += quantity * lastPrice;
            }
        }

        var lastValue = await _context.PortfolioValues
            .Where(pv => pv.PortfolioId == portfolioId)
            .OrderByDescending(pv => pv.Date)
            .FirstOrDefaultAsync();

        if (lastValue?.TotalValue == totalValue)
            return lastValue;

        var newPortfolioValue = new PortfolioValue
        {
            PortfolioId = portfolioId,
            TotalValue = totalValue,
            Date = DateTime.UtcNow
        };

        _context.PortfolioValues.Add(newPortfolioValue);
        await _context.SaveChangesAsync();

        return newPortfolioValue;
    }
    
    public async Task<List<PortfolioValue>> GetPortfolioValuesAsync(int portfolioId)
    {
        var lastCompleteMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1)
            .AddDays(-1)
            .ToUniversalTime();
        var startDate = new DateTime(lastCompleteMonth.Year, lastCompleteMonth.Month, 1)
            .AddMonths(-11)
            .ToUniversalTime();

        var portfolioValues = await _context.PortfolioValues
            .Where(pv => pv.PortfolioId == portfolioId &&
                         pv.Date >= startDate &&
                         pv.Date <= lastCompleteMonth)
            .GroupBy(pv => new { pv.Date.Year, pv.Date.Month })
            .Select(g => new PortfolioValue
            {
                PortfolioId = g.OrderByDescending(x => x.Date).First().PortfolioId,
                Date = g.OrderByDescending(x => x.Date).First().Date,
                TotalValue = g.OrderByDescending(x => x.Date).First().TotalValue
            })
            .OrderBy(pv => pv.Date)
            .ToListAsync();

        return portfolioValues;
    }
    
    public async Task<Dictionary<string, decimal>> GetPortfolioComposition(int portfolioId)
    {
        var investments = await _context.Investments
            .Where(i => i.PortfolioId == portfolioId)
            .Include(i => i.Asset).ThenInclude(asset => asset.AssetType)
            .Include(i => i.Transactions)
            .ToListAsync();

        var groupedInvestments = investments
            .Select(i => new
            {
                AssetType = i.Asset.AssetType,
                TotalValue = i.Transactions.Sum(t =>
                    (t.Type == "BUY" ? 1 : -1) * t.Quantity * t.Price)
            })
            .Where(i => i.TotalValue > 0) 
            .GroupBy(i => i.AssetType.Name)
            .ToDictionary(
                g => g.Key,
                g => g.Sum(i => i.TotalValue)
            );

        var totalPortfolioValue = groupedInvestments.Values.Sum();

        return groupedInvestments.ToDictionary(
            g => g.Key,
            g => totalPortfolioValue == 0 ? 0 : (g.Value / totalPortfolioValue) * 100
        );
    }


}
