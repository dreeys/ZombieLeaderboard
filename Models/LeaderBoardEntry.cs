
using System.Text.Json.Serialization;

namespace ZombieLeaderboard.Models
{
    public class LeaderboardEntry
    {

        public string PlayerName { get; set; } = string.Empty;
        public int Score { get; set; }

    }   
}