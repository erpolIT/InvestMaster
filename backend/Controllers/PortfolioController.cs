using System.Globalization;
using backend.Database;
using backend.Dto;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PortfolioController : ControllerBase
{
    private readonly PortfolioService _portfolioService;
    
    public PortfolioController(PortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetPortfolio(string userId)
    {
        var portfolio = await _portfolioService.GetPortfolioAsync(userId);
        
        if (portfolio == null)
            return NotFound("Portfolio not found.");
        
        return Ok(portfolio);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioDto dto)
    {
        if (string.IsNullOrEmpty(dto.UserId) || string.IsNullOrEmpty(dto.Name))
            return BadRequest("Invalid portfolio data.");

        var newPortfolio = await _portfolioService.CreatePortfolioAsync(dto);

        return CreatedAtAction(nameof(GetPortfolio), new { userId = dto.UserId }, newPortfolio);
    }
    
    [HttpPost("update/{portfolioId}")]
    public async Task<IActionResult> UpdatePortfolioValue(int portfolioId)
    {
        var portfolioValue = await _portfolioService.UpdatePortfolioValueAsync(portfolioId);
        
        if (portfolioValue == null)
            return NotFound("Portfolio not found or has no investments");

        return Ok(portfolioValue);
    }
    
    [HttpGet("user-portfolio/{userId}")]
    public async Task<IActionResult> GetUserPortfolio(string userId)
    {
        var portfolio = await _portfolioService.GetPortfolioAsync(userId);

        if (portfolio == null)
            return NotFound("Portfolio not found");

        return Ok(portfolio.Id);
    }
    
    [HttpGet("portfolio-value/{portfolioId}")]
    public async Task<IActionResult> GetPortfolioValue(int portfolioId)
    {
        var portfolioValues = await _portfolioService.GetPortfolioValuesAsync(portfolioId);
        
        var result = portfolioValues.Select(pv => new PortfolioValueDto
        {
            MonthName = pv.Date.ToString("MMMM", CultureInfo.InvariantCulture),
            Year = pv.Date.Year,
            TotalValue = pv.TotalValue
        }).ToList();

        return Ok(result);
    }
    
    [HttpGet("portfolio-components/{portfolioId}")]
    public async Task<IActionResult> GetPortfolioInvestments(int portfolioId)
    {
        var investments = await _portfolioService.GetPortfolioComposition(portfolioId);
        
        return Ok(investments);
    }
}

public class PortfolioValueDto
{
    public string MonthName { get; set; }
    public int Year { get; set; }
    public decimal TotalValue { get; set; }
}