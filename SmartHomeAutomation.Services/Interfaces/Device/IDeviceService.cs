using System;
using System.Security.Principal;

namespace SmartHomeAutomation.Services.Interfaces.Device
{
    public interface IDeviceService : IReaderService<Domain.Models.DeviceModels.Device>, IWriterService<Domain.Models.DeviceModels.Device>
    {
        Domain.Models.DeviceModels.Device Upsert(Domain.Models.DeviceModels.Device device, IPrincipal userPrincipal);
        Domain.Models.DeviceModels.Device SoftDelete(Guid guid, IPrincipal userPrincipal);
        Domain.Models.DeviceModels.Device CheckForExistingDevice(string device);
    }
}
