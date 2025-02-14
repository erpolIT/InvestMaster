using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllAssets()
    {
        var assets = await _assetService.GetAllAssetsAsync();
        return Ok(assets);
    }

    [HttpGet("{symbol}/{assetTypeId}")]
    public async Task<IActionResult> GetAsset(string symbol, int assetTypeId)
    {
        var asset = await _assetService.GetAssetBySymbolAndTypeAsync(symbol, assetTypeId);
        return Ok(asset);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsset([FromBody] Asset asset)
    {
        await _assetService.UpdateAssetAsync(asset);
        return NoContent();
    }

    [HttpGet("type/{assetTypeId}")]
    public async Task<IActionResult> GetAssetsByType(int assetTypeId)
    {
        var assets = await _assetService.GetAssetsByTypeAsync(assetTypeId);
        return Ok(assets);
    }
}