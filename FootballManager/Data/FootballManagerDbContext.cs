namespace FootballManager.Data
{
    using FootballManager.Data.Models;
    using Microsoft.EntityFrameworkCore;
    public class FootballManagerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<UserPlayer> UserPlayers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=FootballManager;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserPlayer>()
                   .HasKey(up => new { up.UserId, up.PlayerId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Players)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Users)
                .WithOne(u => u.Player)
                .HasForeignKey(u => u.PlayerId);
        }
    }
}
