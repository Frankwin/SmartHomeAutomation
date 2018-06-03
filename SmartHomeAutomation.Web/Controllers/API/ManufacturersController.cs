using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SmartHomeAutomation.Web.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Manufacturers")]
    public class ManufacturersController : Controller
    {
        private readonly SmartHomeAutomationContext context;

        public ManufacturersController(SmartHomeAutomationContext context)
        {
            this.context = context;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return context.Manufacturers.Where(x => !x.IsDeleted);
        }

        // GET: api/Manufacturers/<GUID>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetManufacturer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manufacturer = await context.Manufacturers.SingleOrDefaultAsync(m => m.ManufacturerId == id);

            if (manufacturer == null)
            {
                return NotFound();
            }

            return Ok(manufacturer);
        }

        // PUT: api/Manufacturers/<GUID>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturer([FromRoute] Guid id, [FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != manufacturer.ManufacturerId)
            {
                return BadRequest();
            }

            context.Entry(manufacturer).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(manufacturer);
        }

        // POST: api/Manufacturers
        [HttpPost]
        public async Task<IActionResult> PostManufacturer([FromBody] Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingManufacturer = await GetExistingManufacturer(manufacturer);
            if (existingManufacturer != null && existingManufacturer.IsDeleted)
            {
                context.Entry(existingManufacturer).State = EntityState.Modified;
                existingManufacturer.IsDeleted = false;
            }
            else
            {
                context.Manufacturers.Add(manufacturer);
            }

            await context.SaveChangesAsync();

            return CreatedAtAction("GetManufacturer", new { id = manufacturer.ManufacturerId }, manufacturer);
        }

        // DELETE: api/DeviceCategories/<GUID>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturer([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manufacturer = await context.Manufacturers.SingleOrDefaultAsync(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            manufacturer.IsDeleted = true;
            await context.SaveChangesAsync();

            return Ok(manufacturer);
        }

        private bool ManufacturerExists(Guid id)
        {
            return context.Manufacturers.Any(e => e.ManufacturerId == id);
        }

        private async Task<Manufacturer> GetExistingManufacturer(Manufacturer manufacturer)
        {
            var existingManufacturer = await context.Manufacturers.SingleOrDefaultAsync(m =>
                m.ManufacturerName == manufacturer.ManufacturerName ||
                m.ManufacturerId == manufacturer.ManufacturerId);
            return existingManufacturer;
        }
    }
}