﻿using System;
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
    [Route("api/DeviceCategories")]
    public class DeviceCategoriesController : Controller
    {
        private readonly SmartHomeAutomationContext context;

        public DeviceCategoriesController(SmartHomeAutomationContext context)
        {
            this.context = context;
        }

        // GET: api/DeviceCategories
        [HttpGet]
        public IEnumerable<DeviceCategory> GetDeviceCategories()
        {
            return context.DeviceCategories
                .Where(x => !x.IsDeleted);
                //.Include(a => a.DeviceTypes.Where(y => !y.IsDeleted));
        }

        // GET: api/DeviceCategories/<GUID>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceCategory = await context.DeviceCategories.SingleOrDefaultAsync(m => m.DeviceCategoryId == id);

            if (deviceCategory == null)
            {
                return NotFound();
            }

            return Ok(deviceCategory);
        }

        // PUT: api/DeviceCategories/<GUID>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceCategory([FromRoute] Guid id, [FromBody] DeviceCategory deviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceCategory.DeviceCategoryId)
            {
                return BadRequest();
            }

            context.Entry(deviceCategory).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceCategoryExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return Ok(deviceCategory);
        }

        // POST: api/DeviceCategories
        [HttpPost]
        public async Task<IActionResult> PostDeviceCategory([FromBody] DeviceCategory deviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingDeviceCategory = await GetExistingDeviceCategory(deviceCategory);
            if (existingDeviceCategory != null && existingDeviceCategory.IsDeleted)
            {
                context.Entry(existingDeviceCategory).State = EntityState.Modified;
                existingDeviceCategory.IsDeleted = false;
            }
            else
            {
                context.DeviceCategories.Add(deviceCategory);
            }

            await context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceCategory", new { id = deviceCategory.DeviceCategoryId }, deviceCategory);
        }

        // DELETE: api/DeviceCategories/<GUID>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceCategory = await context.DeviceCategories.SingleOrDefaultAsync(m => m.DeviceCategoryId == id);
            if (deviceCategory == null)
            {
                return NotFound();
            }

            deviceCategory.IsDeleted = true;
            await context.SaveChangesAsync();

            return Ok(deviceCategory);
        }

        private bool DeviceCategoryExists(Guid id)
        {
            return context.DeviceCategories.Any(e => e.DeviceCategoryId == id);
        }

        private async Task<DeviceCategory> GetExistingDeviceCategory(DeviceCategory deviceCategory)
        {
            var existingDeviceCategory = await context.DeviceCategories.SingleOrDefaultAsync(m =>
                m.DeviceCategoryName == deviceCategory.DeviceCategoryName ||
                m.DeviceCategoryId == deviceCategory.DeviceCategoryId);
            return existingDeviceCategory;
        }
    }
}