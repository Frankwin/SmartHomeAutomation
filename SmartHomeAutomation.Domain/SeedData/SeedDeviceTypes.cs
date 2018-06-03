using System.Collections.Generic;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Domain.SeedData
{
    internal static class SeedDeviceTypes
    {
        public static void Seed(SmartHomeAutomationContext dbContext)
        {
            var deviceCategories = dbContext.DeviceCategories;

            var deviceTypes = new List<DeviceType>();
            foreach (var deviceCategory in deviceCategories)
            {
                switch (deviceCategory.DeviceCategoryName)
                {
                    case "Heating/Cooling":
                        deviceTypes.Add(new DeviceType
                        {
                            DeviceTypeName = "Thermostat",
                            DeviceCategoryId = deviceCategory.DeviceCategoryId
                        });
                        deviceTypes.Add(new DeviceType
                        {
                            DeviceTypeName = "Temperature Sensor",
                            DeviceCategoryId = deviceCategory.DeviceCategoryId
                        });
                        break;
                    case "Lighting":
                        deviceTypes.Add(new DeviceType()
                        {
                            DeviceTypeName = "Lightbulb - White",
                            DeviceCategoryId = deviceCategory.DeviceCategoryId
                        });
                        deviceTypes.Add(new DeviceType()
                        {
                            DeviceTypeName = "Lightbulb - Color",
                            DeviceCategoryId = deviceCategory.DeviceCategoryId
                        });
                        break;
                }
            }

            dbContext.DeviceTypes.AddRange(deviceTypes);
            dbContext.SaveChanges();
        }
    }
}
