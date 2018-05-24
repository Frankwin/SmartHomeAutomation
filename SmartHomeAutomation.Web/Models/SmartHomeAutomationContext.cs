using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Web.Models.Accounts;
using SmartHomeAutomation.Web.Models.Devices;
using SmartHomeAutomation.Web.Models.Users;

namespace SmartHomeAutomation.Web.Models
{
    public class SmartHomeAutomationContext : DbContext
    {
        public SmartHomeAutomationContext(DbContextOptions<SmartHomeAutomationContext> options) : base(options)
        { }

        // Account tables
        public DbSet<Account> Accounts { get; set; }

        // User tables
        public DbSet<User> Users { get; set; }

        // Device tables
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<DeviceCategory> DeviceCategories { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }
        
    }
}
