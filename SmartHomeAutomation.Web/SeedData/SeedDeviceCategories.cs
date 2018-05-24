using System.Collections.Generic;
using SmartHomeAutomation.Web.Models;
using SmartHomeAutomation.Web.Models.Devices;

namespace SmartHomeAutomation.Web.SeedData
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
