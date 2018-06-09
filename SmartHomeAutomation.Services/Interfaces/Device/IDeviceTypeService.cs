using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Services.Interfaces.Device
{
    public interface IDeviceTypeService : IReaderService<DeviceType>, IWriterService<DeviceType>
    {
        DeviceType Upsert(DeviceType deviceType, IPrincipal userPrincipal);
        DeviceType SoftDelete(Guid guid, IPrincipal userPrincipal);
        DeviceType CheckForExistingDeviceType(string deviceTypeName);
        PageResult GetAllDevicesForDeviceTypeByPage(Guid deviceType, int pageSize, int currentPage, string sortOrder,
            string direction);
    }
}
