using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Interfaces.Device;

namespace SmartHomeAutomation.Services.Services.Device
{
    public class DeviceService : BaseService<Domain.Models.DeviceModels.Device, SmartHomeAutomationContext>, IDeviceService
    {
        public DeviceService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public Domain.Models.DeviceModels.Device Upsert(Domain.Models.DeviceModels.Device device, IPrincipal userPrincipal)
        {
            var existingDevice = CheckForExistingDevice(device.DeviceName);
            if (existingDevice != null)
            {
                if (existingDevice.IsDeleted)
                {
                    existingDevice.IsDeleted = false;
                    device = existingDevice;
                }
                else
                {
                    throw new DuplicateNameException($"A device with name '{device.DeviceName}' already exists");
                }
            }

            if (device.DeviceId == Guid.Empty)
            {
                device.SetInsertInfo(userPrincipal);
                Insert(device);
            }
            else
            {
                device.SetUpdateInfo(userPrincipal);
                Update(device);
            }

            return device;
        }

        public Domain.Models.DeviceModels.Device SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var device = context.Devices.FirstOrDefault(d => d.DeviceId == guid);
                if (device == null)
                {
                    throw new ArgumentException($"Device with ID {guid} does not exist");
                }
                device.SetDeleteInfo(userPrincipal);
                Update(device);
                return device;
            }
        }

        public Domain.Models.DeviceModels.Device CheckForExistingDevice(string device)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.Devices.FirstOrDefault(x => x.DeviceName.Equals(device, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
