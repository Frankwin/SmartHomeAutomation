using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Services.Interfaces.Device;

namespace SmartHomeAutomation.Api.Controllers.Device
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DevicesController : Controller
    {
        private readonly IDeviceService deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            this.deviceService = deviceService;
        }

        /// <summary>
        /// Get all the devices
        /// </summary>
        /// <returns>List of devices in JSON format</returns>
        [HttpGet]
        public IActionResult GetDevices()
        {
            return Ok(deviceService.GetAll());
        }

        /// <summary>
        /// Get an individual device
        /// </summary>
        /// <param name="guid">Device GUID</param>
        /// <returns>Device object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetDevice([FromRoute] Guid guid)
        {
            var device = deviceService.GetByGuid(guid);

            if (device == null)
            {
                return NotFound();
            }

            return Ok(device);
        }

        /// <summary>
        /// Update a device
        /// </summary>
        /// <param name="guid">Device GUID from URL</param>
        /// <param name="device">Device object from body</param>
        /// <returns>Updated device object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutDevice([FromRoute] Guid guid, [FromBody] Domain.Models.DeviceModels.Device device)
        {
            if (guid != device.DeviceId)
            {
                return BadRequest();
            }

            return Ok(deviceService.Upsert(device, User));
        }

        /// <summary>
        /// Create a new device
        /// </summary>
        /// <param name="device">Device object from body</param>
        /// <returns>Newly created device object in JSON format</returns>
        [HttpPost]
        public IActionResult PostDevice([FromBody] Domain.Models.DeviceModels.Device device)
        {
            return Ok(deviceService.Upsert(device, User));
        }

        /// <summary>
        /// Delete a device
        /// </summary>
        /// <param name="guid">Device GUID from URL</param>
        /// <returns>Deleted device object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteDevice([FromRoute] Guid guid)
        {
            var account = deviceService.SoftDelete(guid, User);
            return Ok(account);
        }
    }
}