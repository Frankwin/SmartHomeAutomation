using System;
using System.Data;
using System.Linq;
using System.Security.Principal;
using SmartHomeAutomation.Domain.Models;
using SmartHomeAutomation.Domain.Models.Settings;
using SmartHomeAutomation.Services.Helpers;
using SmartHomeAutomation.Services.Interfaces;
using SmartHomeAutomation.Services.Interfaces.Settings;

namespace SmartHomeAutomation.Services.Services.Settings
{
    public class RoomService : BaseService<Room, SmartHomeAutomationContext>, IRoomService
    {
        public RoomService(ISmartHomeAutomationService smartHomeAutomationService) : base(smartHomeAutomationService.ConnectionString)
        {
        }

        public Room Upsert(Room room, IPrincipal userPrincipal)
        {
            var existingRoom = CheckForExistingRoom(room.RoomName);
            if (existingRoom != null)
            {
                if (existingRoom.IsDeleted)
                {
                    existingRoom.IsDeleted = false;
                    room = existingRoom;
                }
                else
                {
                    throw new DuplicateNameException($"A room with name '{room.RoomName}' already exists");
                }
            }

            if (room.RoomId == Guid.Empty)
            {
                room.SetInsertInfo(userPrincipal);
                Insert(room);
            }
            else
            {
                room.SetUpdateInfo(userPrincipal);
                Update(room);
            }

            return room;
        }

        public Room SoftDelete(Guid guid, IPrincipal userPrincipal)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                var room = context.Rooms.FirstOrDefault(r => r.RoomId == guid);
                if (room == null)
                {
                    throw new ArgumentException($"Room with ID {guid} does not exist");
                }
                room.SetDeleteInfo(userPrincipal);
                Update(room);
                return room;
            }
        }

        public Room CheckForExistingRoom(string roomName)
        {
            using (var context = new SmartHomeAutomationContext())
            {
                return context.Rooms.FirstOrDefault(x => x.RoomName.Equals(roomName, StringComparison.CurrentCultureIgnoreCase));
            }
        }
    }
}
