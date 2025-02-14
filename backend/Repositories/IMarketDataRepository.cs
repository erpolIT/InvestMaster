using backend.Dto;

namespace backend.Repositories;

public interface IMarketDataRepository
{
    Task SaveMarketDataAsync(MarketDataDto marketData);
    Task<MarketDataDto> GetLatestMarketDataAsync();
    Task<IEnumerable<MarketDataDto>> GetMarketDataHistoryAsync(DateTime from, DateTime to);
}