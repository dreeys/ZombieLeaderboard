using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZombieLeaderboard.Data;
using ZombieLeaderboard.Models;

namespace ZombieLeaderboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly GameDbContext _context;

        public LeaderboardController(GameDbContext context)
        {
            _context = context;
        }

        // GET: api/leaderboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderboardEntry>>> GetLeaderboard()
        {
            var top10 = await _context.Leaderboard
                .OrderByDescending(x => x.Score)
                .Take(10)
                .ToListAsync();

            return Ok(top10);
        }

        // POST: api/leaderboard
        [HttpPost]
        public async Task<IActionResult> AddEntry([FromBody] LeaderboardEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.PlayerName))
                return BadRequest("Player name required.");

            _context.Leaderboard.Add(entry);
            await _context.SaveChangesAsync();
            return Ok(entry);
        }

        // ✅ PUT: api/leaderboard
        // Updates existing entry if player exists, otherwise creates a new one
        [HttpPut]
        public async Task<IActionResult> UpdateOrCreate([FromBody] LeaderboardEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.PlayerName))
                return BadRequest("Invalid entry.");

            var existing = await _context.Leaderboard
                .FirstOrDefaultAsync(e => e.PlayerName == entry.PlayerName);

            if (existing != null)
            {
                // 🧠 Optional: Only update if score is higher
                if (entry.Score > existing.Score)
                {
                    existing.Score = entry.Score;
                    _context.Leaderboard.Update(existing);
                    await _context.SaveChangesAsync();
                    return Ok(existing);
                }

                // Otherwise, return without change
                return Ok(existing);
            }

            // Create new if not found
            _context.Leaderboard.Add(entry);
            await _context.SaveChangesAsync();
            return Ok(entry);
        }

        // DELETE: api/leaderboard
        [HttpDelete]
        public async Task<IActionResult> ResetLeaderboard()
        {
            _context.Leaderboard.RemoveRange(_context.Leaderboard);
            await _context.SaveChangesAsync();
            return Ok("Leaderboard reset successfully!");
        }
    }
}
