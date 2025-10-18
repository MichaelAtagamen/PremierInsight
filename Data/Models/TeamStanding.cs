namespace PremierInsight.Data.Models
{
    // Just the usual getters and setters 
    public class TeamStanding
    {
        public int Position { get; set; }
        public string TeamName { get; set; } = "";
        public int PlayedGames { get; set; }
        public int Points { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
    }
}