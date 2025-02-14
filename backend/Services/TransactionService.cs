using backend.Database;
using backend.Dto;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services;

public class TransactionService
{
    private readonly ApiDbContext _context;
    private readonly AccountBalanceService _accountBalanceService;

    public TransactionService(ApiDbContext context, AccountBalanceService accountBalanceService)
    {
        _context = context;
        _accountBalanceService = accountBalanceService;
    }

    public async Task<bool> CreateTransaction(TransactionDto transactionDto)
    {
        decimal transactionCost = transactionDto.Quantity * transactionDto.Price + transactionDto.Fee;
        
        Console.WriteLine(transactionDto.Type);

        if (transactionDto.Type == "BUY")
        {
            bool hasFunds = await _accountBalanceService.DeductBalance(transactionDto.PortfolioId, transactionCost);
            if (!hasFunds)
                throw new InvalidOperationException("Insufficient funds for this transaction.");
        }
        else if (transactionDto.Type == "SELL")
        {
            await _accountBalanceService.AddBalance(transactionDto.PortfolioId, transactionCost);
        }
        
        var transaction = new Transaction
        {
            InvestmentId = transactionDto.InvestmentId,
            Type = transactionDto.Type,
            Quantity = transactionDto.Quantity,
            Price = transactionDto.Price,
            Fee = transactionDto.Fee,
            Notes = transactionDto.Notes
        };
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}
