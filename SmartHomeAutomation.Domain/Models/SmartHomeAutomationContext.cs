using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Interfaces;
using SmartHomeAutomation.Domain.Models.AccountModels;
using SmartHomeAutomation.Domain.Models.DeviceModels;
using SmartHomeAutomation.Domain.Models.SettingsModels;

namespace SmartHomeAutomation.Domain.Models
{
    public class SmartHomeAutomationContext : IdentityDbContext
    {
        public SmartHomeAutomationContext(DbContextOptions<SmartHomeAutomationContext> options)
            : base(options)
        {
        }

        public SmartHomeAutomationContext()
        { }

        // Account tables
        public DbSet<Account> Accounts { get; set; }

        // User tables
//        public DbSet<User> Users { get; set; }

        // Device tables
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<OwnedDevice> OwnedDevices { get; set;}
        public DbSet<DeviceSetting> DeviceSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
//            builder.Entity<Account>()
//                .HasMany(e => e.Users)
//                .WithOne(e => e.Account);
            builder.Entity<Account>()
                .HasMany(e => e.Rooms)
                .WithOne(e => e.Account);
            builder.Entity<Account>()
                .HasMany(e => e.OwnedDevices)
                .WithOne(e => e.Account);
            builder.Entity<DeviceCategory>()
                .HasMany(e => e.DeviceTypes)
                .WithOne(e => e.DeviceCategory);
            builder.Entity<DeviceType>()
                .HasMany(e => e.Devices)
                .WithOne(e => e.DeviceType);
            builder.Entity<Manufacturer>()
                .HasMany(e => e.Devices)
                .WithOne(e => e.Manufacturer);
            builder.Entity<Room>()
                .HasMany(e => e.LinkedDevices)
                .WithOne(e => e.Room);
            builder.Entity<OwnedDevice>()
                .HasOne(e => e.Device)
                .WithMany(e => e.OwnedDevice)
                .IsRequired(false);
            builder.Entity<OwnedDevice>()
                .HasMany(e => e.DeviceSettings)
                .WithOne(e => e.OwnedDevice);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is ITrackable trackable)
                {
                    var now = DateTime.UtcNow;
                    //var user = GetCurrentUser();
                    var user = "System generated";
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.LastUpdatedAt = now;
                            trackable.LastUpdatedBy = user;
                            break;

                        case EntityState.Added:
                            trackable.CreatedAt = now;
                            trackable.CreatedBy = user;
                            trackable.LastUpdatedAt = now;
                            trackable.LastUpdatedBy = user;
                            trackable.IsDeleted = false;
                            break;
                        case EntityState.Deleted:
                            trackable.IsDeleted = true;
                            break;
                        case EntityState.Detached:
                            break;
                        case EntityState.Unchanged:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(entry.Entity.ToString());
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SmartHomeAutomation;Trusted_Connection=True;ConnectRetryCount=0");
        }

//        private string GetCurrentUser()
//        {
//            var authenticatedUserName = httpContextAccessor?.HttpContext?.User?.Identity?.Name;
//            var authenticatedUserId = httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//
//            return authenticatedUserName ?? authenticatedUserId;
//        }
//        
//        public SmartHomeAutomationContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<SmartHomeAutomationContext>();
//            optionsBuilder.UseSqlServer(
//                "Server=localhost;Database=SmartHomeAutomation;Trusted_Connection=True;ConnectRetryCount=0");
//            return new SmartHomeAutomationContext(optionsBuilder.Options);
//        }
    }
}
