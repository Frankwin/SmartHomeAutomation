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
    public class DeviceSettingService : BaseService<DeviceSetting, SmartHomeAutomationContext>, IDeviceSettingService
    {
        public DeviceSettingService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public DeviceSetting Upsert(DeviceSetting deviceSetting, IPrincipal userPrincipal)
        {
            var existingRoom = CheckForExistingDeviceSetting(deviceSetting.DeviceSettingName);
            if (existingRoom != null)
            {
                if (existingRoom.IsDeleted)
                {
                    existingRoom.IsDeleted = false;
                    deviceSetting = existingRoom;
                }
                else
                {
                    throw new DuplicateNameException($"A Device Setting with name '{deviceSetting.DeviceSettingName}' already exists");
                }
            }

            if (deviceSetting.DeviceSettingId == Guid.Empty)
            {
                deviceSetting.SetInsertInfo(userPrincipal);
                Insert(deviceSetting);
            }
            else
            {
                deviceSetting.SetUpdateInfo(userPrincipal);
                Update(deviceSetting);
            }

            return deviceSetting;
        }

        public DeviceSetting SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var room = context.DeviceSettings.FirstOrDefault(r => r.DeviceSettingId == guid);
                if (room == null)
                {
                    throw new ArgumentException($"Device Setting with ID {guid} does not exist");
                }
                room.SetDeleteInfo(userPrincipal);
                Update(room);
                return room;
            }
        }

        public DeviceSetting CheckForExistingDeviceSetting(string deviceSettingName)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.DeviceSettings.FirstOrDefault(x => x.DeviceSettingName.Equals(deviceSettingName, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
