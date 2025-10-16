using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json; 
using PremierInsight.Data.Models;

namespace PremierInsight.Data
{
    public class FplService
    {
        private readonly HttpClient _httpClient;
        private const string FplApiUrl = "https://fantasy.premierleague.com/api/bootstrap-static/";

        public FplService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FplRoot?> GetFplDataAsync()
        {
            var response = await _httpClient.GetStringAsync(FplApiUrl);
            var data = JsonConvert.DeserializeObject<FplRoot>(response);
            return data;
        }
    }
}