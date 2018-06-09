using System;
using Microsoft.AspNetCore.Mvc;
using SmartHomeAutomation.Domain.Models.DeviceModels;
using SmartHomeAutomation.Services.Interfaces.Device;

namespace SmartHomeAutomation.Api.Controllers.Device
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DeviceCategoriesController : Controller
    {
        private readonly IDeviceCategoryService deviceCategoryService;

        public DeviceCategoriesController(IDeviceCategoryService deviceCategoryService)
        {
            this.deviceCategoryService = deviceCategoryService;
        }

        /// <summary>
        /// Get all the device categories
        /// </summary>
        /// <returns>List of device categories in JSON format</returns>
        [HttpGet]
        public IActionResult GetDeviceCategories()
        {
            return Ok(deviceCategoryService.GetAll());
        }

        /// <summary>
        /// Get an individual device category
        /// </summary>
        /// <param name="guid">Device Category GUID</param>
        /// <returns>Device Category object in JSON format</returns>
        [HttpGet("{guid}")]
        public IActionResult GetDeviceCategory([FromRoute] Guid guid)
        {
            var deviceCategory = deviceCategoryService.GetByGuid(guid);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            return Ok(deviceCategory);
        }

        /// <summary>
        /// Update a device category
        /// </summary>
        /// <param name="guid">Device Category GUID from URL</param>
        /// <param name="deviceCategory">Device Category object from body</param>
        /// <returns>Updated device category object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutDeviceCategory([FromRoute] Guid guid, [FromBody] DeviceCategory deviceCategory)
        {
            if (guid != deviceCategory.DeviceCategoryId)
            {
                return BadRequest();
            }

            return Ok(deviceCategoryService.Upsert(deviceCategory, User));
        }


        /// <summary>
        /// Create a new device category
        /// </summary>
        /// <param name="deviceCategory">Device Category object from body</param>
        /// <returns>Newly created device category object in JSON format</returns>
        [HttpPost]
        public IActionResult PostDeviceCategory([FromBody] DeviceCategory deviceCategory)
        {
            return Ok(deviceCategoryService.Upsert(deviceCategory, User));
        }

        /// <summary>
        /// Delete a device category
        /// </summary>
        /// <param name="guid">Device Category GUID from URL</param>
        /// <returns>Deleted device category object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteDeviceCategory([FromRoute] Guid guid)
        {
            var deviceCategory = deviceCategoryService.SoftDelete(guid, User);
            return Ok(deviceCategory);
        }
    }
}