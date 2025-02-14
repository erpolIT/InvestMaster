using backend.Enums;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.DatabaseSeeder;

public static class DatabaseSeeder
{
    public static void SeedAssets(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssetType>().HasData(
            new AssetType
            {
                Id = (int)AssetTypeId.Crypto,
                Name = "Crypto",
                Description = "Cryptocurrency"
            },
            new AssetType
                {
                    Id = (int)AssetTypeId.Stock,
                    Name = "Stock",
                    Description = "Stocks"
                },
            new AssetType
                {
                    Id = (int)AssetTypeId.ETF,
                    Name = "ETF",
                    Description = "Exchange Traded Funds"
                }
        );
        
        modelBuilder.Entity<Asset>().HasData(
            new Asset 
            { 
                Id = 1,
                Symbol = "BTC",
                Name = "Bitcoin",
                AssetTypeId = (int)AssetTypeId.Crypto,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            },
            new Asset 
            { 
                Id = 2,
                Symbol = "ETH",
                Name = "Ethereum",
                AssetTypeId = (int)AssetTypeId.Crypto,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            },
            new Asset 
            { 
                Id = 3,
                Symbol = "AAPL",
                Name = "Apple Inc.",
                AssetTypeId = (int)AssetTypeId.Stock,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            },
            new Asset 
            { 
                Id = 4,
                Symbol = "MSFT",
                Name = "Microsoft Corporation",
                AssetTypeId = (int)AssetTypeId.Stock,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            },
            new Asset 
            { 
                Id = 5,
                Symbol = "SPY",
                Name = "SPDR S&P 500 ETF Trust",
                AssetTypeId = (int)AssetTypeId.ETF,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            },
            new Asset 
            { 
                Id = 6,
                Symbol = "QQQ",
                Name = "Invesco QQQ Trust",
                AssetTypeId = (int)AssetTypeId.ETF,
                Currency = "USD",
                CurrentPriceOpen = 0,
                CurrentPriceClose = 0
            }
        );

    }
}