using Microsoft.EntityFrameworkCore;
using PersonalVault.Domain.Entities;

namespace PersonalVault.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Note> Notes => Set<Note>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(n => n.Id);
            entity.Property(n => n.Title).IsRequired().HasMaxLength(100);
            entity.Property(n => n.Content).IsRequired();
            entity.Property(n => n.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
    }
}