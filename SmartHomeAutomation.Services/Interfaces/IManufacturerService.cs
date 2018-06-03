using System;
using SmartHomeAutomation.Domain.Models.Device;

namespace SmartHomeAutomation.Services.Interfaces
{
    public interface IManufacturerService : IReadService<Manufacturer>, IWriteService<Manufacturer>
    {
        PagingResult GetDevicesForManufacturer(Guid manufacturerId, int pageSize, int pageNumber, string orderBy,
            string direction);
    }
}
