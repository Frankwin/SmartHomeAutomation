using System;
using System.Security.Principal;

namespace SmartHomeAutomation.Services.Interfaces.Device
{
    public interface IDeviceService : IReaderService<Domain.Models.Device.Device>, IWriterService<Domain.Models.Device.Device>
    {
        Domain.Models.Device.Device Upsert(Domain.Models.Device.Device device, IPrincipal userPrincipal);
        Domain.Models.Device.Device SoftDelete(Guid guid, IPrincipal userPrincipal);
        Domain.Models.Device.Device CheckForExistingDevice(string device);
    }
}
