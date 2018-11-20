using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace EnumIdentifiers.Data.Model
{
    public class DotNetFlixDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SubscriptionLevel> SubscriptionLevels { get; set; }
        public DotNetFlixDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SubscriptionLevel>()
                .ToTable("SubscriptionLevels");

            modelBuilder.Entity<Customer>()
                .ToTable("Customers");

            modelBuilder.Entity<Customer>()
                .Property(c => c.SubscriptionLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Customer>()
                .Property(c => c.Billing)
                .HasConversion<string>();

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.SubscriptionLevelRelation)
                .WithMany()
                .HasForeignKey(c => c.SubscriptionLevel);

            modelBuilder.Entity<SubscriptionLevel>()
                .Property(s => s.Name)
                .HasConversion<string>();

            modelBuilder.Entity<SubscriptionLevel>()
                .Property(s => s.Quality)
                .HasConversion<string>();

            modelBuilder.Entity<SubscriptionLevel>()
                .HasKey(s => s.Name);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscriptionLevel>().HasData(
                new SubscriptionLevel
                {
                    Name = SubscriptionLevel.Level.Basic,
                    PricePerMonth = 7.99m,
                    NumberOfSimultaneousDevices = 1,
                    NumberDevicesWithDownloadCapability = 1,
                    Quality = SubscriptionLevel.VideoQuality.Standard
                },
                new SubscriptionLevel
                {
                    Name = SubscriptionLevel.Level.Standard,
                    PricePerMonth = 10.99m,
                    NumberOfSimultaneousDevices = 2,
                    NumberDevicesWithDownloadCapability = 2,
                    Quality = SubscriptionLevel.VideoQuality.HD
                },
                new SubscriptionLevel
                {
                    Name = SubscriptionLevel.Level.Premium,
                    PricePerMonth = 13.99m,
                    NumberOfSimultaneousDevices = 4,
                    NumberDevicesWithDownloadCapability = 4,
                    Quality = SubscriptionLevel.VideoQuality.UHD
                });
        }
    }
}