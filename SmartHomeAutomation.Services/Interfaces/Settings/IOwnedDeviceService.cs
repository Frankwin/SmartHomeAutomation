using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Settings;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IOwnedDeviceService : IReaderService<OwnedDevice>, IWriterService<OwnedDevice>
    {
        OwnedDevice Upsert(OwnedDevice ownedDevice, IPrincipal userPrincipal);
        OwnedDevice SoftDelete(Guid guid, IPrincipal userPrincipal);
        OwnedDevice CheckForExistingOwnedDeviceName(string name);
    }
}
