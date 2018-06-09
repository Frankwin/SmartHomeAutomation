using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.SettingsModels;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Interfaces.Settings;

namespace SmartHomeAutomation.Services.Services.Settings
{
    public class OwnedDeviceService : BaseService<OwnedDevice, SmartHomeAutomationContext>, IOwnedDeviceService
    {
        public OwnedDeviceService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public OwnedDevice Upsert(OwnedDevice ownedDevice, IPrincipal userPrincipal)
        {
            var existingOwnedDevice = CheckForExistingOwnedDeviceName(ownedDevice.DeviceName);
            if (existingOwnedDevice != null)
            {
                if (existingOwnedDevice.IsDeleted)
                {
                    existingOwnedDevice.IsDeleted = false;
                    ownedDevice = existingOwnedDevice;
                }
                else
                {
                    throw new DuplicateNameException($"A Device with name '{ownedDevice.DeviceName}' already exists for this user");
                }
            }

            if (ownedDevice.OwnedDeviceId == Guid.Empty)
            {
                ownedDevice.SetInsertInfo(userPrincipal);
                Insert(ownedDevice);
            }
            else
            {
                ownedDevice.SetUpdateInfo(userPrincipal);
                Update(ownedDevice);
            }

            return ownedDevice;
        }

        public OwnedDevice SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var ownedDevice = context.OwnedDevices.FirstOrDefault(a => a.OwnedDeviceId == guid);
                if (ownedDevice == null)
                {
                    throw new ArgumentException($"OwnedDevice with ID {guid} does not exist");
                }
                ownedDevice.SetDeleteInfo(userPrincipal);
                Update(ownedDevice);
                return ownedDevice;
            }
        }

        public OwnedDevice CheckForExistingOwnedDeviceName(string name)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var existingOwnedDevice = context.OwnedDevices.FirstOrDefault(x => x.DeviceName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
                return existingOwnedDevice;
            }
        }
    }
}
