using System;
using System.ComponentModel.DataAnnotations;

namespace ZombieLeaderboard.Models
{
    public class LeaderboardEntry
    {
        [Key]                         
        public int Id { get; set; }

        [Required]
        public string PlayerName { get; set; } = string.Empty;

        public int Score { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
