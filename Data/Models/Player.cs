using System.Collections.Generic;

namespace PremierInsight.Data.Models
{
    public class Player
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string web_name { get; set; }
        public int team { get; set; }
        public float now_cost { get; set; }
        public int total_points { get; set; }
    }

    public class FplRoot
    {
        public List<Player> elements { get; set; }
    }
}
