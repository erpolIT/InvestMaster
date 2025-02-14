using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly MarketDataService _service;

    public MarketDataController(MarketDataService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMarketData()
    {
        var result = await _service.GetMarketDataAsync();
        
        return Ok(result);
    }
}