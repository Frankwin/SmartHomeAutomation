using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Domain.Models.SettingsModels;
using SmartHomeAutomation.Services.Interfaces.Settings;

namespace SmartHomeAutomation.Api.Controllers.Settings
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class OwnedDevicesController : Controller
    {
        private readonly IOwnedDeviceService ownedDeviceService;
        
        public OwnedDevicesController(IOwnedDeviceService ownedDeviceService)
        {
            this.ownedDeviceService = ownedDeviceService;
        }

        /// <summary>
        /// Get all the owned devices
        /// </summary>
        /// <returns>List of owned devices in JSON format</returns>
        [HttpGet]
        public IActionResult GetOwnedDevices()
        {
            return Ok(ownedDeviceService.GetAll());
        }

        /// <summary>
        /// Get an individual owned device
        /// </summary>
        /// <param name="guid">Owned Device GUID</param>
        /// <returns>Owned Device object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetOwnedDevice([FromRoute] Guid guid)
        {
            var ownedDevice = ownedDeviceService.GetByGuid(guid);

            if (ownedDevice == null)
            {
                return NotFound();
            }

            return Ok(ownedDevice);
        }

        /// <summary>
        /// Update an Owned Device
        /// </summary>
        /// <param name="guid">Owned Device GUID from URL</param>
        /// <param name="ownedDevice">Owned Device object from body</param>
        /// <returns>Updated Owned Device object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutRoom([FromRoute] Guid guid, [FromBody] OwnedDevice ownedDevice)
        {
            if (guid != ownedDevice.AccountId)
            {
                return BadRequest();
            }

            return Ok(ownedDeviceService.Upsert(ownedDevice, User));
        }

        /// <summary>
        /// Create a new Owned Device
        /// </summary>
        /// <param name="ownedDevice">Owned Device object from body</param>
        /// <returns>Newly created Owned Device object in JSON format</returns>
        [HttpPost]
        public IActionResult PostOwnedDevice([FromBody] OwnedDevice ownedDevice)
        {
            return Ok(ownedDeviceService.Upsert(ownedDevice, User));
        }

        /// <summary>
        /// Delete an Owned Device
        /// </summary>
        /// <param name="guid">Owned Device GUID from URL</param>
        /// <returns>Deleted Owned Device object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteOwnedDevice([FromRoute] Guid guid)
        {
            var account = ownedDeviceService.SoftDelete(guid, User);
            return Ok(account);
        }
    }
}