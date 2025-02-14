using backend.Models;
using backend.Repositories;

namespace backend.Services;

public class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly ILogger<AssetService> _logger;

    public AssetService(IAssetRepository assetRepository, ILogger<AssetService> logger)
    {
        _assetRepository = assetRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
    {
        try
        {
            return await _assetRepository.GetAllAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving all assets.");
            throw;
        }
    }

    // Pobierz aktywo na podstawie symbolu i typu
    public async Task<Asset> GetAssetBySymbolAndTypeAsync(string symbol, int assetTypeId)
    {
        try
        {
            return await _assetRepository.GetBySymbolAndTypeAsync(symbol, assetTypeId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving asset by symbol and type.");
            throw;
        }
    }

    // Zaktualizuj aktywo
    public async Task UpdateAssetAsync(Asset asset)
    {
        try
        {
            await _assetRepository.UpdateAsync(asset);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating asset.");
            throw;
        }
    }

    // Pobierz wszystkie aktywa danego typu
    public async Task<IEnumerable<Asset>> GetAssetsByTypeAsync(int assetTypeId)
    {
        try
        {
            return await _assetRepository.GetAllByTypeAsync(assetTypeId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving assets by type.");
            throw;
        }
    }
}