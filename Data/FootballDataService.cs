using System.Net.Http;
using System.Text.Json;
using PremierInsight.Data.Models;
// I orignally created this cs to hsot my football service API to collect the data for the league table
namespace PremierInsight.Data
{
    public class FootballDataService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FootballDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // It will read from environment variable if set, or use fallback
            _apiKey = Environment.GetEnvironmentVariable("FOOTBALL_DATA_API_KEY")
                      ?? "8f06bf64d29d456aa46bef33945f8e6e"; // My API KEY 
        }

        // Get Premier League standings
        public async Task<List<TeamStanding>> GetStandingsAsync()
        {
            _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);
// I got this from the api football website where they gave me a token
            var response = await _httpClient.GetStringAsync(
                "https://api.football-data.org/v4/competitions/PL/standings");

            using var doc = JsonDocument.Parse(response);
            var standings = new List<TeamStanding>();

            var table = doc.RootElement.GetProperty("standings")[0].GetProperty("table");
            foreach (var t in table.EnumerateArray())
            {
                standings.Add(new TeamStanding
                {
                    Position = t.GetProperty("position").GetInt32(),
                    TeamName = t.GetProperty("team").GetProperty("name").GetString() ?? "Unknown",
                    PlayedGames = t.GetProperty("playedGames").GetInt32(),
                    Points = t.GetProperty("points").GetInt32(),
                    GoalsFor = t.GetProperty("goalsFor").GetInt32(),
                    GoalsAgainst = t.GetProperty("goalsAgainst").GetInt32()
                });
            }

            return standings;
        }

        //  Get upcoming Premier League fixtures
        public async Task<List<Fixture>> GetUpcomingFixturesAsync()
        {
            _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
            _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

            var response = await _httpClient.GetAsync("https://api.football-data.org/v4/competitions/PL/matches");
            var fixtures = new List<Fixture>();

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var matches = doc.RootElement.GetProperty("matches");

                foreach (var match in matches.EnumerateArray().Take(20)) // Limit to 20 for clarity
                {
                    fixtures.Add(new Fixture
                    {
                        HomeTeam = match.GetProperty("homeTeam").GetProperty("name").GetString() ?? "N/A",
                        AwayTeam = match.GetProperty("awayTeam").GetProperty("name").GetString() ?? "N/A",
                        MatchDate = match.GetProperty("utcDate").GetDateTime(),
                        Matchday = match.GetProperty("matchday").GetInt32()
                    }); // Fixture standing 
                }
            }

            return fixtures;
        }
    }
}
