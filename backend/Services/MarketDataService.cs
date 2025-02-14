using backend.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Helpers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace backend.Services;



public class MarketDataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host = "alpha-vantage.p.rapidapi.com";
        private readonly string _apiKey = "b556be3958mshe6803026acbe23cp1f5ec3jsn5fa3cee16bb5";
        

        public MarketDataService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Pobiera dane rynkowe dla kryptowalut, akcji i obligacji, filtrując wyniki do bieżącego dnia.
        /// </summary>
        public async Task<MarketDataDto> GetMarketDataAsync()
        {
            string today = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            
            var cryptoData = await GetCategoryDataAsync(MarketSymbols.CryptoSymbols, "DIGITAL_CURRENCY_DAILY", today);
            var stockData = await GetCategoryDataAsync(MarketSymbols.StockSymbols, "TIME_SERIES_DAILY", today);
            var etfData = await GetCategoryDataAsync(MarketSymbols.EtfSymbols, "TIME_SERIES_DAILY", today);

            return new MarketDataDto
            {
                Date = today,
                Cryptocurrencies = cryptoData,
                Stocks = stockData,
                Etfs = etfData,
                //Bonds = bondData
            };
        }

        /// <summary>
        /// Pobiera dane dla danej kategorii (na podstawie funkcji API i listy symboli).
        /// </summary>
        /// <param name="symbols">Lista symboli do pobrania</param>
        /// <param name="function">Nazwa funkcji API (np. TIME_SERIES_DAILY, DIGITAL_CURRENCY_DAILY)</param>
        /// <param name="date">Data do przefiltrowania wyników</param>
        private async Task<List<SymbolMarketDataDto>> GetCategoryDataAsync(List<string> symbols, string function, string date)
        {
            var results = new List<SymbolMarketDataDto>();
            
            foreach (var symbol in symbols)
            {
                string url;
                if(function == "DIGITAL_CURRENCY_DAILY")
                {
                    url = $"https://{_host}/query?function={function}&symbol={symbol}&market=USD";
                }
                else
                {
                    url = $"https://{_host}/query?function={function}&symbol={symbol}&outputsize=compact&datatype=json";
                }


                try
                {
                    using var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("x-rapidapi-host", _host);
                    request.Headers.Add("x-rapidapi-key", _apiKey);
                    
                    var response = await _httpClient.SendAsync(request);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonString = await response.Content.ReadAsStringAsync();
                        
                        
                        // Deserializujemy do dynamicznego obiektu – lub utwórz model odpowiadający strukturze JSON
                        var data = JsonConvert.DeserializeObject<dynamic>(jsonString);

                        Console.WriteLine(data);

                       
                        string timeSeriesKey = function == "DIGITAL_CURRENCY_DAILY" ? "Time Series (Digital Currency Daily)" : "Time Series (Daily)";
                        if (data[timeSeriesKey] != null)
                        {
                            // Wyszukujemy dane dla danego dnia
                            var dayData = data[timeSeriesKey][date];
                            if (dayData != null)
                            {
                                // Mapujemy dane do DTO – przykład: możemy wyciągnąć open, high, low, close, volume
                                var marketData = new SymbolMarketDataDto
                                {
                                    Symbol = symbol,
                                    Open = (string)dayData["1. open"],
                                    High = (string)dayData["2. high"],
                                    Low = (string)dayData["3. low"],
                                    Close = (string)dayData["4. close"],
                                    Volume = (string)dayData["5. volume"]
                                };
                                results.Add(marketData);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Zaloguj błąd lub podejmij odpowiednie działanie (np. kontynuuj dla kolejnych symboli)
                    Console.WriteLine($"Błąd pobierania danych dla {symbol}: {ex.Message}");
                }
            }

            return results;
        }
}
