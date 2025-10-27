
using System.Text.Json.Serialization;

namespace ZombieLeaderboard.Models
{
    public class LeaderboardEntry
    {
    public int Id { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int Score { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;

    [JsonIgnore] 
    public string FormattedDate => Date.ToString("yyyy-MM-dd");
    }
}