using Microsoft.EntityFrameworkCore;
using ZombieLeaderboard.Models;

namespace ZombieLeaderboard.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }

        public DbSet<LeaderboardEntry> Leaderboard { get; set; }
    }
}