using Microsoft.EntityFrameworkCore;
using MaisonFleurie.Models;

namespace MaisonFleurie.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.Entity<AppUser>().HasIndex(u => u.Email).IsUnique();
}