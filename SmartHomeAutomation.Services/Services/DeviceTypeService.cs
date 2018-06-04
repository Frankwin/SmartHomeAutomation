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
    public class DeviceTypeService : Service<DeviceType, SmartHomeAutomationContext>, IDeviceTypeService
    {
        public DeviceTypeService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public DeviceType GetByDeviceTypeGuid(Guid guid)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.DeviceTypes
                    .SingleOrDefault(x => x.DeviceTypeId == guid);
            }
        }

        public DeviceType Upsert(DeviceType deviceType, IPrincipal userPrincipal)
        {
            var existingDeviceType = CheckForExistingDeviceType(deviceType.DeviceTypeName);
            if (existingDeviceType != null)
            {
                if (existingDeviceType.IsDeleted)
                {
                    existingDeviceType.IsDeleted = false;
                    deviceType = existingDeviceType;
                }
                else
                {
                    throw new DuplicateNameException($"A device type with name '{deviceType.DeviceTypeName}' already exists");
                }
            }

            if (deviceType.DeviceTypeId == Guid.Empty)
            {
                deviceType.SetInsertInfo(userPrincipal);
                Insert(deviceType);
            }
            else
            {
                deviceType.SetUpdateInfo(userPrincipal);
                Update(deviceType);
            }

            return deviceType;
        }

        public DeviceType SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var deviceType = context.DeviceTypes.FirstOrDefault(dc => dc.DeviceTypeId == guid);
                if (deviceType == null)
                {
                    throw new ArgumentException($"Device Type with ID {guid} does not exist");
                }
                deviceType.SetDeleteInfo(userPrincipal);
                Update(deviceType);
                return deviceType;
            }
        }

        public DeviceType CheckForExistingDeviceType(string deviceTypeName)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.DeviceTypes.FirstOrDefault(x => x.DeviceTypeName.Equals(deviceTypeName, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
