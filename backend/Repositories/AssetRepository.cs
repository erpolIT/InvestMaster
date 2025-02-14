using backend.Database;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly ApiDbContext _context;

    public AssetRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asset>> GetAllAsync()
    {
        return await _context.Assets.ToListAsync();
    }
    public async Task<Asset> GetBySymbolAndTypeAsync(string symbol, int assetTypeId)
    {
        return await _context.Assets
            .FirstOrDefaultAsync(a => a.Symbol == symbol && a.AssetTypeId == assetTypeId);
    }

    public async Task UpdateAsync(Asset asset)
    {
        _context.Assets.Update(asset);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Asset>> GetAllByTypeAsync(int assetTypeId)
    {
        return await _context.Assets
            .Where(a => a.AssetTypeId == assetTypeId)
            .ToListAsync();
    }
}