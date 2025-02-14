using backend.Database;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Services;

public class AccountBalanceService
{
    private readonly ApiDbContext _context;

    public AccountBalanceService(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetAvailableBalance(int portfolioId)
    {
        var account = await _context.AccountBalances
            .Where(a => a.PortfolioId == portfolioId)
            .FirstOrDefaultAsync();

        return account?.Balance ?? 0m;
    }

    public async Task<bool> DeductBalance(int portfolioId, decimal amount)
    {
        var account = await _context.AccountBalances
            .Where(a => a.PortfolioId == portfolioId)
            .FirstOrDefaultAsync();

        if (account == null || account.Balance < amount)
            return false;

        account.Balance -= amount;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task AddBalance(int portfolioId, decimal amount)
    {
        var account = await _context.AccountBalances
            .Where(a => a.PortfolioId == portfolioId)
            .FirstOrDefaultAsync();

        if (account != null)
        {
            account.Balance += amount;
            await _context.SaveChangesAsync();
        }
    }

}