using System.Collections.Generic;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Domain.SeedData
{
    internal static class SeedDeviceCategories
    {
        public static void Seed(SmartHomeAutomationContext dbContext)
        {
            var deviceCategories = new List<DeviceCategory>
            {
                new DeviceCategory
                {
                    DeviceCategoryName = "Heating/Cooling"
                },
                new DeviceCategory
                {
                    DeviceCategoryName = "Lighting"
                }
            };
            dbContext.DeviceCategories.AddRange(deviceCategories);
            dbContext.SaveChanges();
        }
    }
}
