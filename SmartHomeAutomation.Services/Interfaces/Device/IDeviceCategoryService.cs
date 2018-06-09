using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Services.Interfaces.Device
{
    public interface IDeviceCategoryService : IReaderService<DeviceCategory>, IWriterService<DeviceCategory>
    {
        DeviceCategory Upsert(DeviceCategory deviceCategory, IPrincipal userPrincipal);
        DeviceCategory SoftDelete(Guid guid, IPrincipal userPrincipal);
    }
}
