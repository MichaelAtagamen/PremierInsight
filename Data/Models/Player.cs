using System.Collections.Generic;

namespace PremierInsight.Data.Models
{
    public class Player
    {
        public int id { get; set; }
        public string first_name { get; set; } = "";
        public string second_name { get; set; } = "";
        public string web_name { get; set; } = "";
        public int team { get; set; }
        public float now_cost { get; set; }
        public int total_points { get; set; }

        // Extra fields from FPL API
        public int goals_scored { get; set; }
        public int assists { get; set; }
        public int minutes { get; set; }
        public int element_type { get; set; } // 1=GK, 2=DEF, 3=MID, 4=FWD
        public string photo { get; set; } = ""; // Added for player images

        // Position display using enum
        public string Position => ((PlayerPosition)element_type).ToString();

        // Optional: Enum form
        public PlayerPosition PositionEnum => (PlayerPosition)element_type;

        // Player face photo (FPL official source)
        public string PhotoUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(photo)) return "";
                var cleaned = photo.Replace(".jpg", "");
                return $"https://resources.premierleague.com/premierleague/photos/players/110x140/p{cleaned}.png";
            }
        }
    }

    // FPL API also provides team data (for lookups)
    public class FplTeam
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string short_name { get; set; } = "";
        public int code { get; set; }
    }

    public class FplRoot
    {
        public List<Player> elements { get; set; } = new();
        public List<FplTeam> teams { get; set; } = new();
    }

    public enum PlayerPosition
    {
        Goalkeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Forward = 4
    }
}