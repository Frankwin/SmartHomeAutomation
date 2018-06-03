using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartHomeAutomation.Entities.Models;
using SmartHomeAutomation.Entities.Models.Device;

namespace SmartHomeAutomation.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/DeviceTypes")]
    public class DeviceTypesController : Controller
    {
        private readonly SmartHomeAutomationContext context;

        public DeviceTypesController(SmartHomeAutomationContext context)
        {
            this.context = context;
        }

        // GET: api/DeviceTypes
        [HttpGet]
        public IEnumerable<DeviceType> GetDeviceTypes()
        {
            return context.DeviceTypes
                .Where(x => !x.IsDeleted)
                .Include(y => y.Devices.Where(a => !a.IsDeleted));
        }

        // GET: api/DeviceTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceType = await context.DeviceTypes.SingleOrDefaultAsync(m => m.DeviceTypeId == id);

            if (deviceType == null)
            {
                return NotFound();
            }

            return Ok(deviceType);
        }

        // PUT: api/DeviceTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceType([FromRoute] Guid id, [FromBody] DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceType.DeviceTypeId)
            {
                return BadRequest();
            }

            context.Entry(deviceType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DeviceTypes
        [HttpPost]
        public async Task<IActionResult> PostDeviceType([FromBody] DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.DeviceTypes.Add(deviceType);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceType", new { id = deviceType.DeviceTypeId }, deviceType);
        }

        // DELETE: api/DeviceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceType([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceType = await context.DeviceTypes.SingleOrDefaultAsync(m => m.DeviceTypeId == id);
            if (deviceType == null)
            {
                return NotFound();
            }

            context.DeviceTypes.Remove(deviceType);
            await context.SaveChangesAsync();

            return Ok(deviceType);
        }

        private bool DeviceTypeExists(Guid id)
        {
            return context.DeviceTypes.Any(e => e.DeviceTypeId == id);
        }
    }
}