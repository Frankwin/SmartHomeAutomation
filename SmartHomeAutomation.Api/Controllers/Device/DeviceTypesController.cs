using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Domain.Models.DeviceModels;
using SmartHomeAutomation.Services.Interfaces.Device;

namespace SmartHomeAutomation.Api.Controllers.Device
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeviceTypesController : Controller
    {
        private readonly IDeviceTypeService deviceTypeService;

        public DeviceTypesController(IDeviceTypeService deviceTypeService)
        {
            this.deviceTypeService = deviceTypeService;
        }

        /// <summary>
        /// Get all the device types
        /// </summary>
        /// <returns>List of device types in JSON format</returns>
        [HttpGet("all")]
        public IActionResult GetDeviceTypes()
        {
            return Ok(deviceTypeService.GetAll());
        }

        /// <summary>
        /// Get an individual device
        /// </summary>
        /// <param name="guid">Device type GUID</param>
        /// <returns>Device type object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetDeviceType([FromRoute] Guid guid)
        {
            var deviceType = deviceTypeService.GetByGuid(guid);

            if (deviceType == null)
            {
                return NotFound();
            }

            return Ok(deviceType);
        }

        [HttpGet("{guid}/devices/{pageSize:int}/{currentPage:int}/{sortOrder:alpha?}/{direction:alpha?}")]
        public IActionResult GetDevicesForDeviceType([FromRoute] Guid guid, [FromRoute] int pageSize, [FromRoute] int currentPage, [FromRoute] string sortOrder, [FromRoute] string direction)
        {
            var devices =
                deviceTypeService.GetByPropertyByPage("Devices", guid, pageSize, currentPage, sortOrder, direction);
            return Ok(devices);
        }

        /// <summary>
        /// Update a device type
        /// </summary>
        /// <param name="guid">Device type GUID from URL</param>
        /// <param name="deviceType">Device type object from body</param>
        /// <returns>Updated device type object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutDeviceType([FromRoute] Guid guid, [FromBody] DeviceType deviceType)
        {
            if (guid != deviceType.DeviceTypeId)
            {
                return BadRequest();
            }

            return Ok(deviceTypeService.Upsert(deviceType, User));
        }

        /// <summary>
        /// Create a new device type
        /// </summary>
        /// <param name="deviceType">Device type object from body</param>
        /// <returns>Newly created device object in JSON format</returns>
        [HttpPost]
        public IActionResult PostDeviceType([FromBody] DeviceType deviceType)
        {
            return Ok(deviceTypeService.Upsert(deviceType, User));
        }

        /// <summary>
        /// Delete a device type
        /// </summary>
        /// <param name="guid">Device type GUID from URL</param>
        /// <returns>Deleted device type object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteDeviceType([FromRoute] Guid guid)
        {
            var account = deviceTypeService.SoftDelete(guid, User);
            return Ok(account);
        }
    }
}