using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.DeviceModels;

namespace SmartHomeAutomation.Services.Interfaces.Device
{
    public interface IManufacturerService : IReaderService<Manufacturer>, IWriterService<Manufacturer>
    {
        Manufacturer Upsert(Manufacturer manufacturer, IPrincipal userPrincipal);
        Manufacturer SoftDelete(Guid guid, IPrincipal userPrincipal);
        Manufacturer CheckForExistingManufacturerName(string name);
    }
}
