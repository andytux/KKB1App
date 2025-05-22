using KKB1App.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace KKB1App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Models.Program> Programs { get; set; }

        public DbSet<Show> Shows { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<TicketHolder> TicketHolders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ticket>()
                .HasIndex(t => new { t.ShowId, t.Row, t.SeatNumber })
                .IsUnique();

            modelBuilder.Entity<Show>()
                .HasIndex(s => s.DateStartTime)
                .IsUnique();
        }
    }
}
