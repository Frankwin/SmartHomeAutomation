using System;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models.Settings;

namespace SmartHomeAutomation.Services.Interfaces.Settings
{
    public interface IRoomService : IReaderService<Room>, IWriterService<Room>
    {
        Room Upsert(Room room, IPrincipal userPrincipal);
        Room SoftDelete(Guid guid, IPrincipal userPrincipal);
        Room CheckForExistingRoom(string roomName);
    }
}
