namespace PremierInsight.Data.Models
{
    public class Fixture
    {
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public DateTime MatchDate { get; set; }
        public int Matchday { get; set; }
    }
}