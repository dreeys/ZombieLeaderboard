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

        public async Task<IActionResult> ResetLeaderboard()
        {
            _context.Leaderboard.RemoveRange(_context.Leaderboard);
            await _context.SaveChangesAsync();
            return Ok("Leaderboard reset successfully!");
        }
    }
}
