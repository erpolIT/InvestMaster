// using backend.Database;
// using backend.Dto;
//
// namespace backend.Repositories;
//
// public class MarketDataRepository : IMarketDataRepository
// {
//     private readonly ApiDbContext _context;
//
//     public MarketDataRepository(ApiDbContext context)
//     {
//         _context = context;
//     }
//
//     public async Task SaveMarketDataAsync(MarketDataDto marketData)
//     {
//         // Konwersja DTO na encje
//         var marketDataEntity = new MarketData
//         {
//             Date = DateTime.Parse(marketData.Date),
//             LastUpdated = DateTime.UtcNow
//         };
//
//         // Mapowanie danych dla każdej kategorii
//         marketDataEntity.Cryptocurrencies = marketData.Cryptocurrencies
//             .Select(c => new SymbolMarketData
//             {
//                 Symbol = c.Symbol,
//                 Open = decimal.Parse(c.Open),
//                 High = decimal.Parse(c.High),
//                 Low = decimal.Parse(c.Low),
//                 Close = decimal.Parse(c.Close),
//                 Volume = decimal.Parse(c.Volume)
//             }).ToList();
//
//         // Podobne mapowanie dla Stocks i ETFs...
//
//         await _context.MarketData.AddAsync(marketDataEntity);
//         await _context.SaveChangesAsync();
//     }
//
//     public async Task<MarketDataDto> GetLatestMarketDataAsync()
//     {
//         var latestData = await _context.MarketData
//             .Include(md => md.Cryptocurrencies)
//             .Include(md => md.Stocks)
//             .Include(md => md.Etfs)
//             .OrderByDescending(md => md.Date)
//             .FirstOrDefaultAsync();
//
//         return MapToDto(latestData);
//     }
//
//     public async Task<IEnumerable<MarketDataDto>> GetMarketDataHistoryAsync(DateTime from, DateTime to)
//     {
//         var historicalData = await _context.MarketData
//             .Include(md => md.Cryptocurrencies)
//             .Include(md => md.Stocks)
//             .Include(md => md.Etfs)
//             .Where(md => md.Date >= from && md.Date <= to)
//             .OrderByDescending(md => md.Date)
//             .ToListAsync();
//
//         return historicalData.Select(MapToDto);
//     }
// }