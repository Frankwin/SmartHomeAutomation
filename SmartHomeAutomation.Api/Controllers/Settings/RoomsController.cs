using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Domain.Models.Settings;
using SmartHomeAutomation.Services.Interfaces.Settings;

namespace SmartHomeAutomation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RoomsController : Controller
    {
        private readonly IRoomService roomService;
        
        public RoomsController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        /// <summary>
        /// Get all the rooms
        /// </summary>
        /// <returns>List of rooms in JSON format</returns>
        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok(roomService.GetAll());
        }

        /// <summary>
        /// Get an individual room
        /// </summary>
        /// <param name="guid">Room GUID</param>
        /// <returns>Room object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetRoom([FromRoute] Guid guid)
        {
            var account = roomService.GetByGuid(guid);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Update an room
        /// </summary>
        /// <param name="guid">Room GUID from URL</param>
        /// <param name="room">Room object from body</param>
        /// <returns>Updated room object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutRoom([FromRoute] Guid guid, [FromBody] Room room)
        {
            if (guid != room.AccountId)
            {
                return BadRequest();
            }

            return Ok(roomService.Upsert(room, User));
        }

        /// <summary>
        /// Create a new room
        /// </summary>
        /// <param name="room">Room object from body</param>
        /// <returns>Newly created room object in JSON format</returns>
        [HttpPost]
        public IActionResult PostRoom([FromBody] Room room)
        {
            return Ok(roomService.Upsert(room, User));
        }

        /// <summary>
        /// Delete an room
        /// </summary>
        /// <param name="guid">Room GUID from URL</param>
        /// <returns>Deleted room object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteRoom([FromRoute] Guid guid)
        {
            var account = roomService.SoftDelete(guid, User);
            return Ok(account);
        }
    }
}