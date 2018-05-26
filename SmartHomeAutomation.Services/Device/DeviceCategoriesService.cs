using System.Collections.Generic;
using SmartHomeAutomation.Entities.Models;
using SmartHomeAutomation.Entities.Models.Device;

namespace SmartHomeAutomation.Services.Device
{
    public class DeviceCategoryService
    {
        private readonly SmartHomeAutomationContext context;

        public DeviceCategoryService(SmartHomeAutomationContext context)
        {
            this.context = context;
        }

        public IEnumerable<DeviceCategory> GetAllDeviceCategories()
        {
            return context.DeviceCategories;
        }
    }
}
