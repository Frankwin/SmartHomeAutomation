using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Domain.Models.Device;
using SmartHomeAutomation.Services.Interfaces;

namespace SmartHomeAutomation.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ManufacturersController : Controller
    {
        private readonly IManufacturerService manufacturerService;

        public ManufacturersController(IManufacturerService manufacturerService)
        {
            this.manufacturerService = manufacturerService;
        }
        
        /// <summary>
        /// Get all the manufacturers
        /// </summary>
        /// <returns>List of Manufacturers in JSON format</returns>
        [HttpGet]
        public IActionResult GetAllManufacturers()
        {
            return Ok(manufacturerService.GetAll());
        }

        /// <summary>
        /// Get a manufacturer
        /// </summary>
        /// <param name="guid">Manufacturer GUID</param>
        /// <returns>Manufacturer object in JSON format</returns>
        [HttpGet("{guid}", Name="GetManufacturer")]
        public IActionResult GetManufacturer([FromRoute] Guid guid)
        {
            var manufacturer = manufacturerService.GetByManufacturerGuid(guid);
            if (manufacturer == null)
            {
                return NotFound();
            }
            return Ok(manufacturer);
        }

        /// <summary>
        /// Create a new manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer object from body</param>
        /// <returns>Newly created manufacturer object in JSON format</returns>
        [HttpPost]
        public IActionResult PostManufacturer([FromBody]Manufacturer manufacturer)
        {
            if (manufacturer == null)
            {
                return BadRequest();
            }

            return Ok(manufacturerService.Upsert(manufacturer, User));
        }

        /// <summary>
        /// Update a manufacturer
        /// </summary>
        /// <param name="guid">Manufacturer GUID from URL</param>
        /// <param name="manufacturer">Manufacturer object from body</param>
        /// <returns>Updated manufacturer object in JSON format</returns>
        [HttpPut("{guid}")]
        public IActionResult PutManufacturer([FromRoute]Guid guid, [FromBody]Manufacturer manufacturer)
        {
            if (guid != manufacturer.ManufacturerId)
            {
                return BadRequest("ID does not match");
            }
            
            return Ok(manufacturerService.Upsert(manufacturer, User));
        }

        /// <summary>
        /// Delete a manufacturer
        /// </summary>
        /// <param name="guid">Manufacturer GUID from URL</param>
        /// <returns>Deleted manufacturer object in JSON format</returns>
        [HttpDelete("{guid}")]
        public IActionResult DeleteManufacturer([FromRoute]Guid guid)
        {
            var manufacturer = manufacturerService.SoftDelete(guid, User);
            return Ok(manufacturer);
        }
    }
}
