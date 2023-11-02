using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<buh> buses { get; set; }
        public DbSet<Driver> drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Bus>()
            //    .HasOne(b => b.Driver)
            //    .WithOne(d => d.Bus)
            //    .HasForeignKey<Bus>(b => b.DriverId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }

    }
}
