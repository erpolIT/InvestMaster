using System.Globalization;
using backend.Dto;
using backend.Enums;
using backend.Repositories;

namespace backend.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class MarketDataSyncService : IHostedService
{
    private readonly ILogger<MarketDataSyncService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private Timer _timer;

    public MarketDataSyncService(ILogger<MarketDataSyncService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    private async Task SyncMarketData(object state)
    {
        // Tworzymy scope, aby móc bezpiecznie używać usług zarejestrowanych jako Scoped
        using (var scope = _serviceProvider.CreateScope())
        {
            // Pobieramy instancje usług z nowego zakresu
            var marketDataService = scope.ServiceProvider.GetRequiredService<MarketDataService>();
            var assetRepository = scope.ServiceProvider.GetRequiredService<IAssetRepository>();

            try
            {
                var marketData = await marketDataService.GetMarketDataAsync();

                // Aktualizacja cen dla poszczególnych kategorii aktywów
                await UpdateAssetPrices(assetRepository, marketData.Cryptocurrencies, (int)AssetTypeId.Crypto);
                await UpdateAssetPrices(assetRepository, marketData.Stocks, (int)AssetTypeId.Stock);
                await UpdateAssetPrices(assetRepository, marketData.Etfs, (int)AssetTypeId.ETF);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Wystąpił błąd podczas synchronizacji danych rynkowych");
            }
        }
    }

    private async Task UpdateAssetPrices(IAssetRepository assetRepository, List<SymbolMarketDataDto> marketData, int assetTypeId)
    {
        foreach (var data in marketData)
        {
            var asset = await assetRepository.GetBySymbolAndTypeAsync(data.Symbol, assetTypeId);
            if (asset != null)
            {
                asset.CurrentPriceOpen = decimal.Parse(data.Open, CultureInfo.InvariantCulture);
                asset.CurrentPriceClose = decimal.Parse(data.Close, CultureInfo.InvariantCulture);
                asset.LastUpdated = DateTime.UtcNow;
                await assetRepository.UpdateAsync(asset);
            }
        }
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Market Data Sync Service jest uruchamiany.");

        // Możemy od razu wykonać synchronizację przy starcie
        await SyncMarketData(null);

        // Ustawiamy timer na synchronizację o określonej godzinie (np. 1:00 AM)
        var now = DateTime.Now;
        var scheduledTime = new DateTime(now.Year, now.Month, now.Day, 1, 0, 0);
        if (now > scheduledTime)
        {
            scheduledTime = scheduledTime.AddDays(1);
        }
        var timeToFirstSync = scheduledTime - now;

        _timer = new Timer(async _ => await SyncMarketData(null),
                           null,
                           timeToFirstSync,
                           TimeSpan.FromHours(24));

        await Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Market Data Sync Service jest zatrzymywany.");

        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}

