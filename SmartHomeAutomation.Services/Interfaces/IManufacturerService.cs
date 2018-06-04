using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IManufacturerService : IReadService<Manufacturer>, IWriteService<Manufacturer>
    {
        Manufacturer GetByManufacturerGuid(Guid manufacturerGuid);
        Manufacturer Upsert(Manufacturer manufacturer, IPrincipal userPrincipal);
        Manufacturer SoftDelete(Guid guid, IPrincipal userPrincipal);
        Manufacturer CheckForExistingManufacturerName(string name);
    }
}
