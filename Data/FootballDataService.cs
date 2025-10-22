using System.Net.Http;
using System.Text.Json;
using PremierInsight.Data.Models;

// I originally created this cs to host my football service API to collect the data for the league table
namespace PremierInsight.Data
{
    public class FootballDataService : IStatsProvider
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

        // Property required by IStatsProvider
        public string SourceName => "Football Data API";

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
                var team = t.GetProperty("team");
                string crest = team.TryGetProperty("crest", out var crestEl) ? (crestEl.GetString() ?? "") : "";

                string? form = null;
                if (t.TryGetProperty("form", out var formEl) && formEl.ValueKind == JsonValueKind.String)
                    form = formEl.GetString();

                standings.Add(new TeamStanding
                {
                    Position = t.GetProperty("position").GetInt32(),
                    TeamName = team.GetProperty("name").GetString() ?? "Unknown",
                    PlayedGames = t.GetProperty("playedGames").GetInt32(),
                    Points = t.GetProperty("points").GetInt32(),
                    GoalsFor = t.GetProperty("goalsFor").GetInt32(),
                    GoalsAgainst = t.GetProperty("goalsAgainst").GetInt32(),
                    GoalDifference = t.GetProperty("goalDifference").GetInt32(),
                    CrestUrl = crest,
                    Form = form
                });
            }

            return standings;
        }

        // Get upcoming Premier League fixtures
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

        // Implemented interface method to test connection
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Remove("X-Auth-Token");
                _httpClient.DefaultRequestHeaders.Add("X-Auth-Token", _apiKey);

                var response = await _httpClient.GetAsync("https://api.football-data.org/v4/competitions/PL");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
