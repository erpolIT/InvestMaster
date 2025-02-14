using backend.Dto;

namespace backend.Services;

public interface IDataService
{
    public Task<MarketDataDto> GetMarketDataAsync();
}