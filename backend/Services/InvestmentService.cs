using backend.Database;
using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class InvestmentService
{
    private readonly ApiDbContext _context;

    public InvestmentService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<Investment> GetInvestmentByIdAsync(int investmentId)
    {
        return await _context.Investments
            .FirstOrDefaultAsync(i => i.Id == investmentId);
    }

    public async Task<Investment> GetInvestmentByAssetAndPortfolioAsync(int assetId, int portfolioId)
    {
        return await _context.Investments
            .FirstOrDefaultAsync(i => i.AssetId == assetId && i.PortfolioId == portfolioId);
    }
    
    
    public async Task<bool> AddInvestmentAsync(AddInvestmentDto investment)
    {
        // Sprawdzamy, czy inwestycja o tym samym AssetId i PortfolioId już istnieje
        var existingInvestment = await GetInvestmentByAssetAndPortfolioAsync(investment.AssetId, investment.PortfolioId);

        if (existingInvestment != null)
        {
            Console.WriteLine("existingInvestment.Entity.Id: " + investment.Id);
            // Inwestycja już istnieje dla tego AssetId i PortfolioId
            return false;
        }

        var newInvestment = _context.Investments.Add(new Investment
        {
            AssetId = investment.AssetId,
            PortfolioId = investment.PortfolioId,
            Transactions = new List<Transaction>()
        });      
        await _context.SaveChangesAsync();

        investment.Id = newInvestment.Entity.Id;
        return true;
    }

    public async Task<bool> AddTransactionAsync(Transaction transaction)
    {
        var investment = await GetInvestmentByIdAsync(transaction.InvestmentId);
        
        if (investment == null)
        {
            // Inwestycja nie istnieje
            return false;
        }

        // Dodaj transakcję do inwestycji
        investment.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Investment>> GetPortfolioInvestmentsAsync(int portfolioId)
    {
        return await _context.Investments
            .Where(i => i.PortfolioId == portfolioId)
            .ToListAsync();
    }
}