using Newtonsoft.Json.Linq;

namespace CapitalsDataFromAPI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Fetch data from an external API - REST Countries and store it in a dictionary
            // https://restcountries.com/#rest-countries

            var client = new HttpClient();
            var url = "https://restcountries.com/v3.1/all?fields=name,capital";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Failed to fetch data from the API.");
                return;
            }
             
            var responseBody = await response.Content.ReadAsStringAsync();
            var data = JArray.Parse(responseBody);
            // Console.WriteLine(data);

            var capitals = new Dictionary<string, string>();

            foreach (var item in data)
            {
                var country = item["name"]?["common"]?.ToString();
                var capital = item["capital"]?.FirstOrDefault()?.ToString();

                if (!string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(capital))
                {
                    capitals[country] = capital;
                }
                else
                {
                    Console.WriteLine($"Missing data for country: {country}");
                }
            }

            Console.WriteLine($"Added {capitals.Count} countries with capitals.");

            foreach (var kvp in capitals)
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
            }
        }
    }
}
