using Microsoft.EntityFrameworkCore;
using PersonalVault.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Register EF Core with connection string from config
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));