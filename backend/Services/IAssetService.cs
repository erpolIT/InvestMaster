using backend.Models;

namespace backend.Services;

public interface IAssetService
{
    Task<IEnumerable<Asset>> GetAllAssetsAsync();
    Task<Asset> GetAssetBySymbolAndTypeAsync(string symbol, int assetTypeId);
    Task UpdateAssetAsync(Asset asset);
    Task<IEnumerable<Asset>> GetAssetsByTypeAsync(int assetTypeId);
}