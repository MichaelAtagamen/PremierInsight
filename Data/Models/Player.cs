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

        // Handy property to show position using enum
        public string Position => ((PlayerPosition)element_type).ToString();
    }

    public class FplRoot
    {
        public List<Player> elements { get; set; } = new();
    }

    // Enum for C# fundamentals
    public enum PlayerPosition
    {
        Goalkeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Forward = 4
    }
}