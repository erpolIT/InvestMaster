using backend.Dto;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvestmentController : ControllerBase
{
    private readonly InvestmentService _investmentService;

    public InvestmentController(InvestmentService investmentService)
    {
        _investmentService = investmentService;
    }

    [HttpGet("check-investment/{investmentId}")]
    public async Task<IActionResult> CheckInvestment(int investmentId)
    {
        var investment = await _investmentService.GetInvestmentByIdAsync(investmentId);
        
        if (investment == null)
        {
            return NotFound("Investment not found.");
        }

        return Ok(investment);
    }

    [HttpPost("add-investment")]
    public async Task<IActionResult> AddInvestment([FromBody] AddInvestmentDto investment)
    {
        var success = await _investmentService.AddInvestmentAsync(investment);
        
        if (!success)
        {
            Console.WriteLine("Investment already exists.");
        }
        return CreatedAtAction(nameof(CheckInvestment),  new { investmentId = investment.Id }, investment);
    }

    [HttpPost("add-transaction")]
    public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
    {
        var success = await _investmentService.AddTransactionAsync(transaction);
        
        if (!success)
        {
            return NotFound("Investment not found.");
        }

        return CreatedAtAction(nameof(CheckInvestment), new { investmentId = transaction.InvestmentId }, transaction);
    }
    
    [HttpGet("portfolio-investments/{portfolioId}")]
    public async Task<IActionResult> GetPortfolioInvestments(int portfolioId)
    {
        var investments = await _investmentService.GetPortfolioInvestmentsAsync(portfolioId);
        
        if (investments == null)
        {
            return NotFound("Portfolio not found or has no investments.");
        }

        return Ok(investments);
    }
}
