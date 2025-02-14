using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AccountBalanceController : ControllerBase
{
    private readonly AccountBalanceService _accountBalanceService;

    public AccountBalanceController(AccountBalanceService accountBalanceService)
    {
        _accountBalanceService = accountBalanceService;
    }
    
    [HttpGet("{portfolioId}")]
    public async Task<IActionResult> GetAccountBalance(int portfolioId)
    {
        var accountBalance = await _accountBalanceService.GetAvailableBalance(portfolioId);
        
        return Ok(accountBalance);
    }
}