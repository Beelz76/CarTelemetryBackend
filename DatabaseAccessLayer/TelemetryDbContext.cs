using DatabaseAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccessLayer
{
    public class TelemetryDbContext : DbContext
    {
        public TelemetryDbContext(DbContextOptions<TelemetryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasKey(x => x.UserId);
            builder.Entity<User>().HasMany(x => x.Cars).WithOne(x => x.User).HasForeignKey("UserId");

            builder.Entity<Car>().HasKey(x => x.CarId);
            builder.Entity<Car>().HasOne(x => x.User).WithMany(x => x.Cars).HasForeignKey("UserId");
        }
    }
}
