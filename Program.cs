using Microsoft.EntityFrameworkCore;
using ZombieLeaderboard.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext for SQLite
builder.Services.AddDbContext<GameDbContext>(options =>
    options.UseSqlite("Data Source=leaderboard.db"));

var app = builder.Build();

// ✅ Optional: recreate DB only once for testing
// Comment out the EnsureDeleted() line after first run
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameDbContext>();

    // db.Database.EnsureDeleted();   // ❌ uncomment ONLY if you want to reset
    db.Database.EnsureCreated();       // ✅ creates if not exists
}

// Configure pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
