using Newtonsoft.Json;

namespace backend.Dto;

public class MarketDataDto
{
    public string Date { get; set; }
    public List<SymbolMarketDataDto> Cryptocurrencies { get; set; }
    public List<SymbolMarketDataDto> Stocks { get; set; }
    public List<SymbolMarketDataDto> Bonds { get; set; }
    public List<SymbolMarketDataDto> Etfs { get; set; }
}

public class SymbolMarketDataDto
{
    public string Symbol { get; set; }
    public string Open { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    public string Close { get; set; }
    public string Volume { get; set; }
}