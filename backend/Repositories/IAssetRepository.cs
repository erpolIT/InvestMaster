using backend.Models;

namespace backend.Repositories;

public interface IAssetRepository
{
    Task<IEnumerable<Asset>> GetAllAsync();
    Task<Asset> GetBySymbolAndTypeAsync(string symbol, int assetTypeId);
    Task UpdateAsync(Asset asset);
    Task<IEnumerable<Asset>> GetAllByTypeAsync(int assetTypeId);
}