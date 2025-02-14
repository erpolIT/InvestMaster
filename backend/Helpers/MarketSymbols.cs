namespace backend.Helpers;

public static class MarketSymbols
{
    public static List<string> CryptoSymbols => new List<string>
    {
        "BTC", "ETH",
        // "XRP", "LTC",
        //"BCH", "ADA", "DOT", "LINK", "XLM", "DOGE",
        //"SOL", "UNI", "AVAX", "MATIC", "ATOM", "ALGO", "VET", "FIL", "ICP", "EOS"
    };

    public static List<string> StockSymbols => new List<string>
    {
        "AAPL", "MSFT",
        //"GOOGL", "AMZN", "FB",
        //"TSLA", "BRK.A", "NVDA", "JPM", "V",
        //"JNJ", "WMT", "PG", "DIS", "MA", "HD", "BAC", "VZ", "ADBE", "NFLX"
    };

    public static List<string> EtfSymbols => new List<string>
    {
        "SPY", "QQQ",
    };
    
    
}