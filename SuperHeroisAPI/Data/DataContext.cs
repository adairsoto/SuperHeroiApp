using Microsoft.EntityFrameworkCore;
using SuperHeroisAPI.Models;

namespace SuperHeroisAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Heroi> Herois { get; set; } 
        public DbSet<SuperPoderes> SuperPoderes { get; set; }
        public DbSet<HeroiSuperPoderes> HeroiSuperPoderes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroiSuperPoderes>()
                .HasKey(hsp => new { hsp.HeroiId, hsp.SuperPoderesId });

            modelBuilder.Entity<HeroiSuperPoderes>()
                .HasOne(hsp => hsp.Heroi)
                .WithMany(h => h.HeroiSuperPoderes)
                .HasForeignKey(hsp => hsp.HeroiId);

            modelBuilder.Entity<HeroiSuperPoderes>()
                .HasOne(hsp => hsp.SuperPoderes)
                .WithMany(sp => sp.HeroiSuperPoderes)
                .HasForeignKey(hsp => hsp.SuperPoderesId);
        }
    }
}
