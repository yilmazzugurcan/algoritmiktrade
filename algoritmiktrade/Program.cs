using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace algoritmiktrade
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var symbol = "BTCUSDT";
            var url = $"https://api.binance.com/api/v3/ticker/price?symbol={symbol}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);

            var jsonString = await response.Content.ReadAsStringAsync();

            var priceData = JsonSerializer.Deserialize<PriceData>(jsonString);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error getting price data. Status code: {response.StatusCode}");
                return;
            }
            Console.WriteLine(jsonString);

           // var jsonString = @"{""symbol"":""BTCUSDT"",""price"":""27357.88000000""}";
            var PriceData = JsonSerializer.Deserialize<PriceData>(jsonString);
            var lastPrice = priceData.Price;
            Console.WriteLine($"Last price for {symbol}: {lastPrice}");
        }

        class PriceData
        {
            public decimal Price { get; set; }
        }
    }
}