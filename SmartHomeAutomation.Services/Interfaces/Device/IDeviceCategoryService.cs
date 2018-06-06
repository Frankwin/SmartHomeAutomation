using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IDeviceCategoryService : IReaderService<DeviceCategory>, IWriterService<DeviceCategory>
    {
        DeviceCategory Upsert(DeviceCategory deviceCategory, IPrincipal userPrincipal);
        DeviceCategory SoftDelete(Guid guid, IPrincipal userPrincipal);
    }
}
