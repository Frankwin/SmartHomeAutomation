using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Settings;

namespace SmartHomeAutomation.Services.Interfaces.Settings
{
    public interface IDeviceSettingService : IReaderService<DeviceSetting>, IWriterService<DeviceSetting>
    {
        DeviceSetting Upsert(DeviceSetting deviceSetting, IPrincipal userPrincipal);
        DeviceSetting SoftDelete(Guid guid, IPrincipal userPrincipal);
        DeviceSetting CheckForExistingDeviceSetting(string deviceSettingName);
    }
}
