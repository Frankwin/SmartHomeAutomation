using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Device;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Services.Services
{
    public class DeviceCategoryService : Service<DeviceCategory, SmartHomeAutomationContext>, IDeviceCategoryService
    {
        public DeviceCategoryService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public DeviceCategory Upsert(DeviceCategory deviceCategory, IPrincipal userPrincipal)
        {
            var existingDeviceCategory = CheckForExistingDeviceCategory(deviceCategory.DeviceCategoryName);
            if (existingDeviceCategory != null)
            {
                if (existingDeviceCategory.IsDeleted)
                {
                    existingDeviceCategory.IsDeleted = false;
                    deviceCategory = existingDeviceCategory;
                }
                else
                {
                    throw new DuplicateNameException($"A device category with name '{deviceCategory.DeviceCategoryName}' already exists");
                }
            }

            if (deviceCategory.DeviceCategoryId == Guid.Empty)
            {
                deviceCategory.SetInsertInfo(userPrincipal);
                Insert(deviceCategory);
            }
            else
            {
                deviceCategory.SetUpdateInfo(userPrincipal);
                Update(deviceCategory);
            }

            return deviceCategory;
        }

        public DeviceCategory SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var deviceCategory = context.DeviceCategories.FirstOrDefault(dc => dc.DeviceCategoryId == guid);
                if (deviceCategory == null)
                {
                    throw new ArgumentException($"Device Category with ID {guid} does not exist");
                }
                deviceCategory.SetDeleteInfo(userPrincipal);
                Update(deviceCategory);
                return deviceCategory;
            }
        }

        public DeviceCategory CheckForExistingDeviceCategory(string name)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.DeviceCategories.FirstOrDefault(x => x.DeviceCategoryName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
