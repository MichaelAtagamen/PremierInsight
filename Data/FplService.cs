using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PremierInsight.Data.Models;

namespace PremierInsight.Data
{
    public class FplService : IStatsProvider
    {
        private readonly HttpClient _httpClient;
        private const string FplApiUrl = "https://fantasy.premierleague.com/api/bootstrap-static/";

        // Property required by IStatsProvider
        public string SourceName => "Fantasy Premier League API";

        public FplService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch full FPL dataset
        public async Task<FplRoot?> GetFplDataAsync()
        {
            var response = await _httpClient.GetStringAsync(FplApiUrl);
            return JsonConvert.DeserializeObject<FplRoot>(response);
        }

        // Implemented interface method: test if the API is reachable
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(FplApiUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}