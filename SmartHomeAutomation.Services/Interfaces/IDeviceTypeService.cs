using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IDeviceTypeService : IReadService<DeviceType>, IWriteService<DeviceType>
    {
        DeviceType GetByDeviceTypeGuid(Guid guid);
        DeviceType Upsert(DeviceType deviceType, IPrincipal userPrincipal);
        DeviceType SoftDelete(Guid guid, IPrincipal userPrincipal);
        DeviceType CheckForExistingDeviceType(string deviceTypeName);
    }
}
