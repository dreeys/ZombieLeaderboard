using Microsoft.EntityFrameworkCore;
using ZombieLeaderboard.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<GameDbContext>(opt =>
    opt.UseSqlite("Data Source=/tmp/leaderboard.db"));

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ Always enable Swagger (even in Production)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Skapa DB vid uppstart
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GameDbContext>();
    db.Database.EnsureDeleted();  // 🧹 Tar bort gammal databas
    db.Database.EnsureCreated();  // 🆕 Skapar ny med rätt schema
}

app.Run();
